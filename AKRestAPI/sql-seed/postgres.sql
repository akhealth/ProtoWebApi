CREATE TABLE people (id SERIAL, name varchar, enabled boolean);
INSERT INTO people (name, enabled) VALUES('clint',true),('simon',true),('mark',true);
SELECT * FROM people;