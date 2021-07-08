package com.example.android_app_mobile_am;

import android.os.Bundle;
import android.view.View;
import android.widget.EditText;

import androidx.appcompat.app.AppCompatActivity;


public class ConfigActivity extends AppCompatActivity {

    private EditText ip_val;
    private EditText port_val;
    private EditText api_val;
    private EditText sample_val;

    private long stime;
    private String  api;
    private String port;
    private String ip;


    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_config);

        ip_val =findViewById(R.id.e_ip);
        port_val = findViewById(R.id.e_port);
        api_val = findViewById(R.id.e_api);
        sample_val = findViewById(R.id.e_sample);

        ip_val.setText(ConfigParams.IP_param);
        port_val.setText(ConfigParams.Port_param);
        api_val.setText(ConfigParams.API_param);
        long st_l = ConfigParams.SampleTime_param;
        String st_str = String.valueOf(st_l);
        sample_val.setText(st_str);

    }

    public void set_config_params(View view){
        String st_str;


        st_str= sample_val.getText().toString();
        stime = Long.parseLong(st_str);
        ConfigParams.set_SampleTime_param(stime);

        ip = ip_val.getText().toString();
        ConfigParams.set_IP_param(ip);
    }


    public void config_back(View view){
        finish();
    }
}
