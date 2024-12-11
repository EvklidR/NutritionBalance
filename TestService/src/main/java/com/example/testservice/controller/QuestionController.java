package com.example.testservice.controller;

import com.example.testservice.model.Answer;
import com.example.testservice.model.Question;
import com.example.testservice.service.QuestionService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/questions")
public class QuestionController {
    @Autowired
    private QuestionService questionService;

    @GetMapping
    public List<Question> getAllQuestions() {
        return questionService.findAll();
    }

    @PostMapping
    public Question createQuestion(@RequestBody Question question) {
        for (Answer answer : question.getAnswers()) {
            answer.setQuestion(question); // Устанавливаем связь
        }
        return questionService.save(question);
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteQuestion(@PathVariable Long id) {
        questionService.deleteById(id);
        return ResponseEntity.noContent().build();
    }
}