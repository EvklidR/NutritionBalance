package com.example.testservice.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpEntity;
import org.springframework.http.ResponseEntity;

@Service
public class ApiService {

    private final RestTemplate restTemplate;

    @Autowired
    public ApiService(RestTemplate restTemplate) {
        this.restTemplate = restTemplate;
    }

    public void changeRoleIfEvaluationExceeds(double evaluationResult, String token) {
        if (evaluationResult > 0.9) {

            String url = "";

            // Выполнение запроса к внешнему сервису
            HttpHeaders headers = new HttpHeaders();
            headers.set("Authorization", "Bearer " + token);
            HttpEntity<String> entity = new HttpEntity<>(headers);

            ResponseEntity<String> response = restTemplate.postForEntity("http://localhost:7078/authorisation-service/Auth/change-role/", entity, String.class);
        }
    }
}