-- I enforce the inclusion of IDs to avoid disparities between countries 
-- and their corresponding states, and likewise for the respective cities.

INSERT INTO countries (id, name) VALUES 
(1, 'Colombia'), 
(2, 'United States'), 
(3, 'España');

-- This is necessary for Postgres to retrieve the sequence
SELECT setval(pg_get_serial_sequence('countries', 'id'), (SELECT MAX(id) FROM countries));

INSERT INTO states (id, name, country_id) VALUES 
-- Colombia (ID 1):
(1, 'Bolívar', 1), (2, 'Antioquia', 1), (3, 'Cundinamarca', 1),
-- United States (ID 2):
(4, 'Florida', 2), (5, 'California', 2), (6, 'New York', 2),
-- España (ID 3):
(7, 'Madrid', 3), (8, 'Cataluña', 3), (9, 'Andalucía', 3);

SELECT setval(pg_get_serial_sequence('states', 'id'), (SELECT MAX(id) FROM states));

INSERT INTO cities (id, name, state_id) VALUES 
-- Bolívar (ID 1)
(1, 'Cartagena', 1), (2, 'Magangué', 1), (3, 'Turbaco', 1),
-- Antioquia (ID 2)
(4, 'Medellín', 2), (5, 'Envigado', 2), (6, 'Itagüí', 2),
-- Cundinamarca (ID 3)
(7, 'Bogotá', 3), (8, 'Soacha', 3), (9, 'Chía', 3),
-- Florida (ID 4)
(10, 'Miami', 4), (11, 'Orlando', 4), (12, 'Tampa', 4),
-- California (ID 5)
(13, 'Los Angeles', 5), (14, 'San Francisco', 5), (15, 'San Diego', 5),
-- New York (ID 6)
(16, 'New York City', 6), (17, 'Buffalo', 6), (18, 'Rochester', 6),
-- Madrid (ID 7)
(19, 'Madrid', 7), (20, 'Alcalá de Henares', 7), (21, 'Getafe', 7),
-- Cataluña (ID 8)
(22, 'Barcelona', 8), (23, 'Girona', 8), (24, 'Tarragona', 8),
-- Andalucía (ID 9)
(25, 'Sevilla', 9), (26, 'Granada', 9), (27, 'Málaga', 9);

SELECT setval(pg_get_serial_sequence('cities', 'id'), (SELECT MAX(id) FROM cities));