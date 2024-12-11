package com.example.testservice.controller;

import com.example.testservice.service.EvaluationService;
import com.example.testservice.service.ApiService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/evaluate")
public class EvaluationController {

    @Autowired
    private EvaluationService evaluationService;

    @Autowired
    private ApiService apiService;

    @PostMapping
    public double evaluateAnswers(@RequestBody List<Long> selectedIds, String token) {
        double result = evaluationService.evaluateAnswers(selectedIds);
        apiService.changeRoleIfEvaluationExceeds(result, token);
        return result;
    }
}