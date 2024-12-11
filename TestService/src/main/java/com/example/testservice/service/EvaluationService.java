package com.example.testservice.service;

import com.example.testservice.model.Answer;
import com.example.testservice.repository.AnswerRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class EvaluationService {

    @Autowired
    private AnswerRepository answerRepository;

    public double evaluateAnswers(List<Long> selectedIds) {
        int score = 0;
        int correctAnswersCount = 0;

        List<Answer> allAnswers = answerRepository.findAll();

        for (Answer answer : allAnswers) {
            if (answer.isCorrect() && selectedIds.contains(answer.getId())) {
                score++;
                correctAnswersCount++;
            } else if (!answer.isCorrect() && selectedIds.contains(answer.getId())) {
                score--;
            } else if (answer.isCorrect() && !selectedIds.contains(answer.getId())) {
                score--;
            }
        }

        return score > 0 ? (double) score / correctAnswersCount : 0;
    }
}