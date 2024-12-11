package com.example.testservice.controller;

import com.example.testservice.model.Answer;
import com.example.testservice.service.AnswerService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/answers")
public class AnswerController {
    @Autowired
    private AnswerService answerService;

    @GetMapping
    public List<Answer> getAllAnswers() {
        return answerService.findAll();
    }

    @PostMapping
    public Answer createAnswer(@RequestBody Answer answer) {
        return answerService.save(answer);
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteAnswer(@PathVariable Long id) {
        answerService.deleteById(id);
        return ResponseEntity.noContent().build();
    }
}