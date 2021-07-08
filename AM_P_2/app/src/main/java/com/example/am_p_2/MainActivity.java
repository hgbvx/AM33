package com.example.am_p_2;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.os.CountDownTimer;
import android.view.View;
import android.view.WindowManager;
import android.widget.EditText;
import android.widget.TextView;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;
import com.jjoe64.graphview.GraphView;
import com.jjoe64.graphview.series.DataPoint;
import com.jjoe64.graphview.series.LineGraphSeries;

import org.json.JSONObject;

public class MainActivity extends AppCompatActivity {

    private GraphView graph_temp;
    private LineGraphSeries<DataPoint> temp_series;
    private GraphView graph_pres;
    private LineGraphSeries<DataPoint> pres_series;
    private GraphView graph_inte;
    private LineGraphSeries<DataPoint> inte_series;
    private CountDownTimer ct;
    private RequestQueue queue;
    private int i=1;
    private int j=1;
    private int k=1;
    private EditText stime_et;
    private long stime=1000;

    private TextView temp_val;
    private TextView press_val;
    private TextView inte_val;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        temp_val = (TextView) findViewById(R.id.list_temp);
        press_val = (TextView) findViewById(R.id.list_press);
        inte_val = (TextView) findViewById(R.id.list_inte);

        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_HIDDEN);

        queue = Volley.newRequestQueue( MainActivity.this);

        //stime_et = findViewById(R.id.sample_time);

        graph_temp = (GraphView)findViewById(R.id.graph_temp);
        temp_series = new LineGraphSeries<>(new DataPoint[]{
                new DataPoint(0,0)
        });
        temp_series.setColor(Color.parseColor("#FFF18F01"));
        graph_temp.addSeries(temp_series);
        graph_temp.getViewport().setXAxisBoundsManual(true);
        graph_temp.getViewport().setMinX(0);
        graph_temp.getViewport().setMaxX(10);
        graph_temp.getViewport().setYAxisBoundsManual(true);
        graph_temp.getViewport().setMinY(0);
        graph_temp.getViewport().setMaxY(100);

        graph_pres = (GraphView)findViewById(R.id.graph_press);
        pres_series = new LineGraphSeries<>(new DataPoint[]{
                new DataPoint(0,0)
        });
        pres_series.setColor(Color.parseColor("#FFDDD92A"));
        graph_pres.addSeries(pres_series);
        graph_pres.getViewport().setXAxisBoundsManual(true);
        graph_pres.getViewport().setMinX(0);
        graph_pres.getViewport().setMaxX(10);
        graph_pres.getViewport().setYAxisBoundsManual(true);
        graph_pres.getViewport().setMinY(975);
        graph_pres.getViewport().setMaxY(1050);

        graph_inte = (GraphView)findViewById(R.id.graph_inte);
        inte_series = new LineGraphSeries<>(new DataPoint[]{
                new DataPoint(0,0)
        });
        inte_series.setColor(Color.parseColor("#FFE2C044"));
        graph_inte.addSeries(inte_series);
        graph_inte.getViewport().setXAxisBoundsManual(true);
        graph_inte.getViewport().setMinX(0);
        graph_inte.getViewport().setMaxX(10);
        graph_inte.getViewport().setYAxisBoundsManual(true);
        graph_inte.getViewport().setMinY(0);
        graph_inte.getViewport().setMaxY(100);

    }

    public void timer_met(){
        String URL = "http://192.168.0.11/";
        JsonObjectRequest jsonObjectRequest = new JsonObjectRequest
                (Request.Method.GET, URL, null, new Response.Listener<JSONObject>() {

                    @Override
                    public void onResponse(JSONObject response) {
                        String tps = response.optString("temperature");
                        double tp_read = Double.parseDouble(tps);
                        DataPoint tp = new DataPoint(i, tp_read);
                        temp_series.appendData(tp, true, 11);
                        graph_temp.addSeries(temp_series);
                        temp_val.setText(tps);
                        i++;
                        String pps = response.optString("pressure");
                        double pp_read = Double.parseDouble(pps);
                        DataPoint pp = new DataPoint(j, pp_read);
                        pres_series.appendData(pp, true, 11);
                        graph_pres.addSeries(pres_series);
                        press_val.setText(pps);
                        j++;
                        String inps = response.optString("light");
                        double inp_read = Double.parseDouble(inps);
                        DataPoint inp = new DataPoint(k, inp_read);
                        inte_series.appendData(inp, true, 11);
                        graph_inte.addSeries(inte_series);
                        inte_val.setText(inps);
                        k++;
                    }
                }, new Response.ErrorListener() {

                    @Override
                    public void onErrorResponse(VolleyError error) {
                        // TODO: Handle error

                    }
                });
        queue.add(jsonObjectRequest);

        ct.start();
    }

    public void temp_start(View view){
        ct = new CountDownTimer(stime,20) {
            @Override
            public void onTick(long millisUntilFinished) {

            }

            @Override
            public void onFinish() {
                timer_met();
            }
        }.start();
    }

    public void temp_stop(View view){
        ct.cancel();
    }

    public void reset_temp_graph(View view){
        temp_series.resetData(new DataPoint[]{
                new DataPoint(i,0)
        });
    }

    public void reset_pres_graph(View view){
        pres_series.resetData(new DataPoint[]{
                new DataPoint(j,0)
        });
    }

    public void set_sample_time(View view){
        String st_str;
        st_str= stime_et.getText().toString();
        stime = Long.parseLong(st_str);
    }

    public void config(View view){
        startActivity(new Intent(MainActivity.this,Config.class));
    }
}