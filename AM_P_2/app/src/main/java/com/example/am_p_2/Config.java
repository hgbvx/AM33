package com.example.am_p_2;

import android.content.Context;
import android.graphics.Color;
import android.os.Bundle;
import android.os.Environment;
import android.util.Log;
import android.view.View;
import android.view.WindowManager;
import android.widget.EditText;

import androidx.appcompat.app.AppCompatActivity;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FileWriter;
import java.io.IOException;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.nio.file.StandardOpenOption;

public class Config extends AppCompatActivity {

    private EditText ip_val;
    private EditText port_val;
    private EditText api_val;
    private EditText sample_val;

    private long stime;
    private int  api;
    private int port;
    private int ip;

    private String ip_str;
    String port_str;
    String api_str;
    String sample_str;

    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.config);

        ip_val =findViewById(R.id.e_ip);
        port_val = findViewById(R.id.e_port);
        api_val = findViewById(R.id.e_api);
        sample_val = findViewById(R.id.e_sample);

    }
    public void setIp_val(View view){

        ip_str = ip_val.getText().toString();

    }
    public void setPort_val(View view){

        port_str = port_val.getText().toString();
    }
    public void setApi_val(View view){

        api_str = ip_val.getText().toString();
    }
    public void setSample_val(View view){

        sample_str = ip_val.getText().toString();
    }


    public void givenWritingStringToFile_whenUsingPrintWriter_thenCorrect() throws IOException {
        String path = "C:/Users/Lenovo/AndroidStudioProjects/AM_P_2/config.txt";
        FileWriter fileWriter = new FileWriter(path);
    }



    public void writeToFile(String content) {
        try {
            File file = new File("config.txt");

            FileWriter writer = new FileWriter(file);
            writer.append(content);
            //writer.flush();
            writer.close();
        } catch (IOException e) {
        }
    }



    public void writeToFile2(String content) {
        FileWriter fWriter;
        File sdCardFile = new File("config.txt");
        //Log.d("TAG", sdCardFile.getPath()); //<-- check the log to make sure the path is correct.
        try{
            fWriter = new FileWriter(sdCardFile, true);
            fWriter.write("hi");
            fWriter.flush();
            fWriter.close();
        }catch(Exception e){
            e.printStackTrace();
        }
    }
    public void save_config(View view){
        String str = "Hello";
        writeToFile2(str);
    }






    public void config_back(View view){
        finish();
    }
}



