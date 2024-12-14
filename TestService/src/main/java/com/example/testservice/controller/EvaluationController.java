package com.example.testservice.controller;

import com.example.testservice.service.EvaluationService;
import com.example.testservice.service.ApiService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.io.IOException;
import java.security.KeyManagementException;
import java.security.NoSuchAlgorithmException;
import java.util.List;

@RestController
@RequestMapping("/evaluate")
public class EvaluationController {

    @Autowired
    private EvaluationService evaluationService;

    @Autowired
    private ApiService apiService;

    @PostMapping
    public double evaluateAnswers(
            @RequestBody List<Long> selectedIds,
            @RequestHeader("Authorization") String authorizationHeader
    ) throws NoSuchAlgorithmException, IOException, InterruptedException, KeyManagementException {

        double result = evaluationService.evaluateAnswers(selectedIds);
        String token = authorizationHeader.startsWith("Bearer ")
                ? authorizationHeader.substring(7)
                : authorizationHeader;
        apiService.changeRoleIfEvaluationExceeds(result, token);
        return result;
    }
}
