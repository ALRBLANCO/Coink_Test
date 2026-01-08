-- Registro de usuario
CREATE OR REPLACE PROCEDURE sp_register_user(
    p_full_name VARCHAR,
    p_phone VARCHAR,
    p_address TEXT,
    p_city_id INT
)
LANGUAGE plpgsql
AS $$
BEGIN
    -- Business logic: Insert user
    INSERT INTO users (full_name, phone, address, city_id)
    VALUES (p_full_name, p_phone, p_address, p_city_id);
    
END;
$$;

-- Obtener Países
CREATE OR REPLACE FUNCTION fn_get_countries()
RETURNS TABLE(id INT, name VARCHAR) AS $$
BEGIN
    RETURN QUERY SELECT c.id, c.name FROM countries c;
END;
$$ LANGUAGE plpgsql;

-- Obtener Estados por País
CREATE OR REPLACE FUNCTION fn_get_states_by_country(p_country_id INT)
RETURNS TABLE(id INT, name VARCHAR, country_id INT) AS $$
BEGIN
    RETURN QUERY SELECT s.id, s.name, s.country_id 
    FROM states s 
    WHERE s.country_id = p_country_id;
END;
$$ LANGUAGE plpgsql;

-- Obtener Ciudades por Estado
CREATE OR REPLACE FUNCTION fn_get_cities_by_state(p_state_id INT)
RETURNS TABLE(id INT, name VARCHAR, state_id INT) AS $$
BEGIN
    RETURN QUERY SELECT ci.id, ci.name, ci.state_id 
    FROM cities ci 
    WHERE ci.state_id = p_state_id;
END;
$$ LANGUAGE plpgsql;


-- Función para validar si existe una ciudad
CREATE OR REPLACE FUNCTION fn_exists_city(p_id INT)
RETURNS BOOLEAN AS $$
BEGIN
    RETURN EXISTS(SELECT 1 FROM cities WHERE id = p_id);
END;
$$ LANGUAGE plpgsql;

-- Función para validar si existe un estado
CREATE OR REPLACE FUNCTION fn_exists_state(p_id INT)
RETURNS BOOLEAN AS $$
BEGIN
    RETURN EXISTS(SELECT 1 FROM states WHERE id = p_id);
END;
$$ LANGUAGE plpgsql;

-- Función para validar si existe un país
CREATE OR REPLACE FUNCTION fn_exists_country(p_id INT)
RETURNS BOOLEAN AS $$
BEGIN
    RETURN EXISTS(SELECT 1 FROM countries WHERE id = p_id);
END;
$$ LANGUAGE plpgsql;