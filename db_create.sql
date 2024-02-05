DROP DATABASE IF EXISTS ROTDK;
CREATE DATABASE ROTDK;

CREATE TABLE SaveData (
    SaveID      INT           AUTO_INCREMENT PRIMARY KEY,
    Name        VARCHAR(50)   NOT NULL,
    Date        TIMESTAMP     NOT NULL,
    Position    VARCHAR(50)   NOT NULL,
    SceneName   VARCHAR(50)   NOT NULL
)