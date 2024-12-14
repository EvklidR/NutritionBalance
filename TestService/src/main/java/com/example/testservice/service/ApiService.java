package com.example.testservice.service;

import com.example.testservice.service.model.InsecureHttpClient;

import org.springframework.stereotype.Service;
import java.io.IOException;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.security.KeyManagementException;
import java.security.NoSuchAlgorithmException;

@Service
public class ApiService {

    public void changeRoleIfEvaluationExceeds(double evaluationResult, String token) throws NoSuchAlgorithmException, KeyManagementException, IOException, InterruptedException {
        if (evaluationResult > 0.9) {

            String url = "https://localhost:7184/Auth/change-role";

            HttpClient client = InsecureHttpClient.create();

            HttpRequest request = HttpRequest.newBuilder()
                    .uri(URI.create(url))
                    .header("Authorization", "Bearer " + token)
                    .POST(HttpRequest.BodyPublishers.noBody())
                    .build();

            HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());

            System.out.println("Response Code: " + response.statusCode());
            System.out.println("Response Body: " + response.body());
        }
    }
}
