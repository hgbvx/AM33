package com.example.android_app_mobile_am;

import androidx.appcompat.app.AppCompatActivity;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.os.CountDownTimer;
import android.util.Log;
import android.view.View;
import android.view.WindowManager;
import android.widget.EditText;
import android.widget.Switch;
import android.widget.TextView;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.jjoe64.graphview.GraphView;
import com.jjoe64.graphview.series.DataPoint;
import com.jjoe64.graphview.series.LineGraphSeries;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.security.SecureRandom;
import java.security.cert.X509Certificate;
import java.text.BreakIterator;

import javax.net.ssl.HostnameVerifier;
import javax.net.ssl.HttpsURLConnection;
import javax.net.ssl.SSLContext;
import javax.net.ssl.SSLSession;
import javax.net.ssl.TrustManager;
import javax.net.ssl.X509TrustManager;

public class MainActivity extends AppCompatActivity {

    private GraphView graph_temp;
    private LineGraphSeries<DataPoint> temp_series;
    private GraphView graph_pres;
    private LineGraphSeries<DataPoint> pres_series;
    private GraphView graph_inte;
    private LineGraphSeries<DataPoint> inte_series;

    private TextView temp_val;
    private TextView press_val;
    private TextView inte_val;

    private Switch temp_sw;
    private Switch pres_sw;
    private Switch inte_sw;
    private Switch ip_sw;

    private CountDownTimer ct;
    private CountDownTimer lt;

    private RequestQueue queue;

    private int i=1;
    private int j=1;
    private int k=1;
    private long stime_calc;

    public void timer_list_met(){
        String URL = "http://" + ConfigParams.IP_param + "/get_all_sensors.php";
        JsonArrayRequest jsonArrayRequest = new JsonArrayRequest
                (Request.Method.GET, URL, null, new Response.Listener<JSONArray>() {

                    @Override
                    public void onResponse(JSONArray response) {
                        try {
                            JSONObject temperature = (JSONObject) response.get(0);
                            String tps = temperature.getString("value");
                            temp_val.setText(tps);

                            JSONObject pressure = (JSONObject) response.get(1);
                            String pps = pressure.getString("value");
                            press_val.setText(pps);

                            JSONObject intensity = (JSONObject) response.get(2);
                            String inps = intensity.getString("value");
                            inte_val.setText(inps);
                        }
                        catch (JSONException e) {
                            e.printStackTrace();
                        }

                    }
                }, new Response.ErrorListener() {

                    @Override
                    public void onErrorResponse(VolleyError error) {
                        Log.d("MyActivity",String.valueOf(error));

                    }
                });
        queue.add(jsonArrayRequest);

        lt.start();
    }

    public void list_start(){
        stime_calc = ConfigParams.SampleTime_param;
        lt = new CountDownTimer(stime_calc,20) {
            @Override
            public void onTick(long millisUntilFinished) {

            }

            @Override
            public void onFinish() {
                timer_list_met();
            }
        }.start();
    }

    public void list_stop(){
        lt.cancel();
    }


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        HttpsTrustManager.allowAllSSL();

        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_HIDDEN);

        temp_val = (TextView) findViewById(R.id.list_temp);
        press_val = (TextView) findViewById(R.id.list_press);
        inte_val = (TextView) findViewById(R.id.list_inte);

        temp_sw = findViewById(R.id.sb_temp);
        pres_sw = findViewById(R.id.sb_press);
        inte_sw = findViewById(R.id.sb_inte);
        ip_sw = findViewById(R.id.sb_ip);

        queue = Volley.newRequestQueue( MainActivity.this);

        list_start();

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
        graph_temp.getViewport().setMinY(20);
        graph_temp.getViewport().setMaxY(40);

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
        graph_inte.getViewport().setMaxY(1000);


    }

    public void timer_met(){
        String URL = "http://" + ConfigParams.IP_param + "/get_all_sensors.php";
        JsonArrayRequest jsonArrayRequest = new JsonArrayRequest
                (Request.Method.GET, URL, null, new Response.Listener<JSONArray>() {

                    @Override
                    public void onResponse(JSONArray response) {
                        try {
                            JSONObject temperature = (JSONObject) response.get(0);
                            String tps = temperature.getString("value");
                            double tp_read = Double.parseDouble(tps);
                            DataPoint tp = new DataPoint(i, tp_read);
                            temp_series.appendData(tp, true, 11);
                            graph_temp.addSeries(temp_series);
                            temp_val.setText(tps);
                            i++;

                            JSONObject pressure = (JSONObject) response.get(1);
                            String pps = pressure.getString("value");
                            double pp_read = Double.parseDouble(pps);
                            DataPoint pp = new DataPoint(j, pp_read);
                            pres_series.appendData(pp, true, 11);
                            graph_pres.addSeries(pres_series);
                            press_val.setText(pps);
                            j++;

                            JSONObject intensity = (JSONObject) response.get(2);
                            String inps = intensity.getString("value");
                            double inp_read = Double.parseDouble(inps);
                            DataPoint inp = new DataPoint(k, inp_read);
                            inte_series.appendData(inp, true, 11);
                            graph_inte.addSeries(inte_series);
                            inte_val.setText(inps);
                            k++;
                        }
                        catch (JSONException e) {
                            e.printStackTrace();
                        }

                    }
                }, new Response.ErrorListener() {

                    @Override
                    public void onErrorResponse(VolleyError error) {
                        Log.d("MyActivity",String.valueOf(error));

                    }
                });
        queue.add(jsonArrayRequest);

        ct.start();
    }

    public void temp_start(View view){
        stime_calc = ConfigParams.SampleTime_param;
        ct = new CountDownTimer(stime_calc,20) {
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

    public void config(View view){
        startActivity(new Intent(MainActivity.this,ConfigActivity.class));
    }

    public void reset_graphs(View view){
        temp_series.resetData(new DataPoint[]{
                new DataPoint(i,0)
        });
        pres_series.resetData(new DataPoint[]{
                new DataPoint(j,0)
        });
        inte_series.resetData(new DataPoint[]{
                new DataPoint(k,0)
        });
    }

    public void send_screen(View view) {
        String arg1 = "", arg2 = "", arg3 = "", arg4 = "";
        if (temp_sw.isChecked()) {
            arg1 = "arg1=t";
        }
        if (pres_sw.isChecked()) {
            if (arg1.isEmpty()) {
                arg2 = "arg2=p";
            } else {
                arg2 = "&arg2=p";
            }
        }
        if (inte_sw.isChecked()) {
            if (arg1.isEmpty() && arg2.isEmpty()) {
                arg3 = "arg3=l";
            } else {
                arg3 = "&arg3=l";
            }
        }
        if (ip_sw.isChecked()) {
            if (arg1.isEmpty() && arg2.isEmpty() && arg3.isEmpty()) {
                arg4 = "arg4=i";
            } else {
                arg4 = "&arg4=i";
            }
        }
        RequestQueue queue = Volley.newRequestQueue(this);

        String URL = "http://" + ConfigParams.IP_param + "/post_display.php/?" + arg1 + arg2 + arg3 + arg4;
        StringRequest stringRequest = new StringRequest(Request.Method.GET, URL,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        // Display the first 500 characters of the response string.
                        BreakIterator textView;

                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
            }
        });

// Add the request to the RequestQueue.
        queue.add(stringRequest);
    }
}