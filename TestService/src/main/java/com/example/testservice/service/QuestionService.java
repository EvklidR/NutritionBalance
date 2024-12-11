package com.example.testservice.service;

import com.example.testservice.model.Answer;
import com.example.testservice.model.Question;
import com.example.testservice.repository.QuestionRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class QuestionService {
    @Autowired
    private QuestionRepository questionRepository;

    public List<Question> findAll() {
        return questionRepository.findAll();
    }

    public Question save(Question question) {

        return questionRepository.save(question);
    }

    public void deleteById(Long id) {
        questionRepository.deleteById(id);
    }
}