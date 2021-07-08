package com.example.android_app_mobile_am;

public class ConfigParams {

    static long SampleTime_param=1000;
    static String IP_param="192.168.0.102";
    static String  Port_param = "25565";
    static String API_param = "8.11";

    public static void set_SampleTime_param(long stp)
    {
        SampleTime_param = stp;
    }
    public static void set_IP_param(String ipp)
    {
        IP_param = ipp;
    }
}
