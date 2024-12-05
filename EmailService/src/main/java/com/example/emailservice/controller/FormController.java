package com.example.emailservice.controller;

import com.example.emailservice.service.EmailService;
import org.springframework.web.bind.annotation.*;

import jakarta.mail.MessagingException;

@RestController
@RequestMapping("/form")
public class FormController {

    private final EmailService emailService;

    public FormController(EmailService emailService) {
        this.emailService = emailService;
    }

    @PostMapping("/send")
    public String sendForm(@RequestParam String email, @RequestParam String formLink) {
        try {
            emailService.sendFormLink(email, formLink);
            return "Form link sent successfully to " + email;
        } catch (MessagingException e) {
            e.printStackTrace();
            return "Failed to send form link.";
        }
    }
}
