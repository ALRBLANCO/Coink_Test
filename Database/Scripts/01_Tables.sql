-- Create Countries Table
CREATE TABLE countries (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE states (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    country_id INT NOT NULL,
    CONSTRAINT fk_country FOREIGN KEY (country_id) REFERENCES countries(id) ON DELETE CASCADE
);

CREATE TABLE cities (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    state_id INT NOT NULL,
    CONSTRAINT fk_state FOREIGN KEY (state_id) REFERENCES states(id) ON DELETE CASCADE
);

-- Contemple la posibilidad de dejar el campo phone como unico
-- pero en la practica si puede repetir, sobre todo si es fijo.
CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    full_name VARCHAR(100) NOT NULL,
    phone VARCHAR(20) NOT NULL,
    address TEXT NOT NULL,
    city_id INT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_city FOREIGN KEY (city_id) REFERENCES cities(id)
);

-- Indexes for performance
CREATE INDEX idx_users_city_id ON users(city_id);
CREATE INDEX idx_states_country_id ON states(country_id);
CREATE INDEX idx_cities_state_id ON cities(state_id);