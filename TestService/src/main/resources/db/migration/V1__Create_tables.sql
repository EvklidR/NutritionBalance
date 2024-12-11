-- src/main/resources/db/migration/V1__Create_tables.sql
CREATE TABLE question (
                          id BIGINT PRIMARY KEY IDENTITY(1,1),
                          question_text VARCHAR(255) NOT NULL
);

CREATE TABLE answer (
                        id BIGINT PRIMARY KEY IDENTITY(1,1),
                        text VARCHAR(255) NOT NULL,
                        is_correct BIT NOT NULL,
                        question_id BIGINT NOT NULL,
                        FOREIGN KEY (question_id) REFERENCES question(id)
);