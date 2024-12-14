package com.example.testservice.service.model;

import javax.net.ssl.*;
import java.net.http.HttpClient;
import java.security.NoSuchAlgorithmException;
import java.security.KeyManagementException;

public class InsecureHttpClient {
    public static HttpClient create() throws NoSuchAlgorithmException, KeyManagementException {
        // Создаем контекст SSL, который игнорирует все проверки
        SSLContext sslContext = SSLContext.getInstance("TLS");
        sslContext.init(null, new TrustManager[]{new X509TrustManager() {
            public void checkClientTrusted(java.security.cert.X509Certificate[] chain, String authType) {}
            public void checkServerTrusted(java.security.cert.X509Certificate[] chain, String authType) {}
            public java.security.cert.X509Certificate[] getAcceptedIssuers() { return null; }
        }}, new java.security.SecureRandom());

        // Создаем HTTP клиент с этим SSL контекстом
        return HttpClient.newBuilder()
                .sslContext(sslContext)
                .build();
    }
}
