INSERT INTO physical_values(id, name, designation, description) VALUES (1, 'Температура', 'T', 'Температура');
INSERT INTO physical_values(id, name, designation, description) VALUES (2, 'Давление', 'P', 'Давление');
INSERT INTO physical_values(id, name, designation, description) VALUES (3, 'Объем', 'V', 'Объем');
INSERT INTO physical_values(id, name, designation, description) VALUES (4, 'Сила тока', 'I', 'Сила тока');
INSERT INTO physical_values(id, name, designation, description) VALUES (5, 'Электрическое напряжение', 'U', 'Электрическое напряжение');
INSERT INTO physical_values(id, name, designation, description) VALUES (6, 'Масса', 'm', 'Масса');
INSERT INTO physical_values(id, name, designation, description) VALUES (7, 'Частота', 'f', 'Частота');
INSERT INTO physical_values(id, name, designation, description) VALUES (8, 'Плотность', 'ρ', 'Плотность');
INSERT INTO physical_values(id, name, designation, description) VALUES (9, 'Длина', 'L', 'Длина');
INSERT INTO physical_values(id, name, designation, description) VALUES (10, 'Электрическое сопротивление', 'R', 'Электрическое сопротивление');
INSERT INTO physical_values(id, name, designation, description) VALUES (11, 'Вязкость', 'υ', 'Вязкость');
INSERT INTO physical_values(id, name, designation, description) VALUES (12, 'Теплоемкость', 'C', 'Теплоемкость');
INSERT INTO physical_values(id, name, designation, description) VALUES (13, 'Время', 't', 'Время');
INSERT INTO physical_values(id, name, designation, description) VALUES (14, 'Скорость', 'v', 'Скорость');
INSERT INTO physical_values(id, name, designation, description) VALUES (15, 'Ускорение', 'a', 'Ускорение');

INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (1, 'МПа', 'МПа', 'Мегапаскаль', (SELECT id FROM physical_values AS pv WHERE pv.designation = 'P'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (2, 'Куб.м', 'м3', 'Кубический метр', (SELECT id FROM physical_values AS pv WHERE pv.designation = 'V'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (3, 'Гц', 'Гц', 'Герц', (SELECT id FROM physical_values AS pv WHERE pv.designation = 'f'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (4, '%', '%', 'Процент', NULL);
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (5, 'ºC', 'ºC', 'Градус цельсия', (SELECT id FROM physical_values AS pv WHERE pv.designation = 'T'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (6, 'В', 'В', 'Вольт', (SELECT id FROM physical_values AS pv WHERE pv.designation = 'U'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (7, 'г/куб.см', 'г/см3', 'Грам на кубический сантиметр', (SELECT id FROM physical_values AS pv WHERE pv.designation = 'ρ'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (8, 'об/мин', 'об/мин', 'Оборотов в минуту', (SELECT id FROM physical_values AS pv WHERE pv.designation = 'v'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (9, 'А', 'А', 'Ампер', (SELECT id FROM physical_values AS pv WHERE pv.designation = 'I'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (10, 'В', 'В', 'Вольт', (SELECT id FROM physical_values AS pv WHERE pv.designation = 'U'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (11, 'м', 'м', 'Метр', (SELECT id FROM physical_values AS pv WHERE pv.designation = 'L'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (12, 'мм', 'мм', 'Миллиметр', (SELECT id FROM physical_values AS pv WHERE pv.designation = 'L'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (13, 'т/сут', 'т/сут', 'Тонн в сутки', NULL);
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (14, 'куб.м/сут', 'м3/сут', 'Кубических метров в сутки', NULL);
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (15, 'млн. куб.м/сут', '', 'Миллионов кубических метров в сутки', NULL);
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (16, 'м/с2', 'м/с2', 'Метр/секунду в квадрате', (SELECT id FROM physical_values AS pv WHERE pv.designation = 'a'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (17, 'МОм', 'МОм', 'Мегаомм', (SELECT id FROM physical_values AS pv WHERE pv.designation = 'R'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (18, 'мм2', 'мм2', 'Квадратный миллиметр', NULL);
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (19, 'об', 'об', 'Оборот', NULL);
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (20, 'мПа/с', 'мПа/с', 'Миллипаскаль в секунду', NULL);
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (21, 'м3/(сут*МПа)', '', 'Кубических метров на мегапаскаль в сутки', NULL);
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (22, '', '', 'Безразмерная величина', NULL);
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (23, 'кг/куб.м', 'кг/куб.м', 'Килограм на кубический метр', (SELECT id FROM physical_values AS pv WHERE pv.designation = 'ρ'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (24, 'Па*с', 'Па*с', 'Паскаль*секунда', (SELECT id FROM physical_values AS pv WHERE pv.designation = 'υ'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (25, 'м3/т', 'м3/т', 'Кубических метров в тонну', null);
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (26, 'кДж/(кг*ºC)', '', 'Килоджоуль на килограм и градус', (SELECT id FROM physical_values AS pv WHERE pv.designation = 'C'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (27, 'час', 'час', 'Час', (SELECT id FROM physical_values AS pv WHERE pv.designation = 't'));

INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (1, 'Температура помещения А точка 1', '', 'double', 'zone_a_temp_p1, zone_a', 
        (SELECT id FROM measure_units AS m WHERE m.name = 'ºC'), 300);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (2, 'Температура помещения А точка 2', '', 'double', 'zone_a_temp_p2, zone_a',
        (SELECT id FROM measure_units AS m WHERE m.name = 'ºC'), 300);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (3, 'Температура помещения А точка 3', '', 'double', 'zone_a_temp_p3, zone_a',
        (SELECT id FROM measure_units AS m WHERE m.name = 'ºC'), 300);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (4, 'Температура помещения B точка 1', '', 'double', 'zone_b_temp_p1, zone_b',
        (SELECT id FROM measure_units AS m WHERE m.name = 'ºC'), 300);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (5, 'Температура помещения B точка 2', '', 'double', 'zone_b_temp_p2, zone_b',
        (SELECT id FROM measure_units AS m WHERE m.name = 'ºC'), 300);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (6, 'Температура помещения B точка 3', '', 'double', 'zone_b_temp_p3, zone_b',
        (SELECT id FROM measure_units AS m WHERE m.name = 'ºC'), 300);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (7, 'Температура в электрощитовой точка 1', '', 'double', 'zone_с_temp_p1, zone_с',
        (SELECT id FROM measure_units AS m WHERE m.name = 'ºC'), 300);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (8, 'Температура в электрощитовой точка 2', '', 'double', 'zone_с_temp_p2, zone_с',
        (SELECT id FROM measure_units AS m WHERE m.name = 'ºC'), 300);

INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (9, 'Влажность помещения А точка 1', '', 'double', 'zone_a_moisture_p1, zone_a',
        (SELECT id FROM measure_units AS m WHERE m.name = '%'), 300);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (10, 'Влажность помещения А точка 2', '', 'double', 'zone_a_moisture_p2, zone_a',
        (SELECT id FROM measure_units AS m WHERE m.name = '%'), 300);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (11, 'Влажность помещения А точка 3', '', 'double', 'zone_a_moisture_p3, zone_a',
        (SELECT id FROM measure_units AS m WHERE m.name = '%'), 300);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (12, 'Влажность помещения B точка 1', '', 'double', 'zone_b_moisture_p1, zone_b',
        (SELECT id FROM measure_units AS m WHERE m.name = '%'), 300);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (13, 'Влажность помещения B точка 2', '', 'double', 'zone_b_moisture_p2, zone_b',
        (SELECT id FROM measure_units AS m WHERE m.name = '%'), 300);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (14, 'Влажность помещения B точка 3', '', 'double', 'zone_b_moisture_p3, zone_b',
        (SELECT id FROM measure_units AS m WHERE m.name = '%'), 300);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (15, 'Влажность в электрощитовой точка 1', '', 'double', 'zone_с_moisture_p1, zone_с',
        (SELECT id FROM measure_units AS m WHERE m.name = '%'), 300);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (16, 'Влажность в электрощитовой точка 2', '', 'double', 'zone_с_moisture_p2, zone_с',
        (SELECT id FROM measure_units AS m WHERE m.name = '%'), 300);

INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (17, 'Твердые частицы в помещении А точка 1', '', 'double', 'zone_a_particles_in_air_p1, zone_a',
        (SELECT id FROM measure_units AS m WHERE m.name = '%'), 300);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (18, 'Твердые частицы в помещении А точка 2', '', 'double', 'zone_a_particles_in_air_p2, zone_a',
        (SELECT id FROM measure_units AS m WHERE m.name = '%'), 300);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (19, 'Твердые частицы в помещении А точка 3', '', 'double', 'zone_a_particles_in_air_p3, zone_a',
        (SELECT id FROM measure_units AS m WHERE m.name = '%'), 300);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (20, 'Твердые частицы в помещении B точка 1', '', 'double', 'zone_b_particles_in_air_p1, zone_b',
        (SELECT id FROM measure_units AS m WHERE m.name = '%'), 300);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (21, 'Твердые частицы в помещении B точка 2', '', 'double', 'zone_b_particles_in_air_p2, zone_b',
        (SELECT id FROM measure_units AS m WHERE m.name = '%'), 300);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (22, 'Твердые частицы в помещении B точка 3', '', 'double', 'zone_b_particles_in_air_p3, zone_b',
        (SELECT id FROM measure_units AS m WHERE m.name = '%'), 300);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (23, 'Твердые частицы в электрощитовой точка 1', '', 'double', 'zone_с_particles_in_air_p1, zone_с',
        (SELECT id FROM measure_units AS m WHERE m.name = '%'), 300);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (24, 'Твердые частицы в электрощитовой точка 2', '', 'double', 'zone_d_particles_in_air_p2, zone_с',
        (SELECT id FROM measure_units AS m WHERE m.name = '%'), 300);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (23, 'Твердые частицы на входе точка 1', '', 'double', 'zone_d_particles_in_air_p1, zone_d',
        (SELECT id FROM measure_units AS m WHERE m.name = '%'), 300);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (24, 'Твердые частицы на входе точка 2', '', 'double', 'zone_d_particles_in_air_p2, zone_d',
        (SELECT id FROM measure_units AS m WHERE m.name = '%'), 300);

INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (25, 'Отключение / включение электроэнергии на линии точка 1', '', 'bool', 'zone_d_power_supply_p1, zone_d',
        (SELECT id FROM measure_units AS m WHERE m.name = ''), 0);
INSERT INTO parameters(id, name, description, type, aliases, measure_unit_id, frequency)
VALUES (26, 'Отключение / включение электроэнергии на линии точка 2', '', 'bool', 'zone_d_power_supply_p1, zone_d',
        (SELECT id FROM measure_units AS m WHERE m.name = ''), 0);

INSERT INTO parameters_values(parameter_id, time, VALUE)
VALUES (1, '2025-04-10 10:00:00', '18.1'),
       (1, '2025-04-10 11:00:00', '18.2'),
       (1, '2025-04-10 12:00:00', '18.1'),
       (1, '2025-04-10 13:00:00', '18.1'),
       (1, '2025-04-10 14:00:00', '18.3'),
       (1, '2025-04-10 15:00:00', '18.2'),
       (1, '2025-04-10 16:00:00', '18.1'),
       (1, '2025-04-10 17:00:00', '18.2'),
       (1, '2025-04-10 18:00:00', '18.1'),
       (1, '2025-04-10 19:00:00', '18.2'),
       (1, '2025-04-10 20:00:00', '18.1'),
       (1, '2025-04-10 21:00:00', '18.0'),
       (1, '2025-04-10 22:00:00', '18.0'),
       (1, '2025-04-10 23:00:00', '18.1'),
       (1, '2025-04-11 00:00:00', '18.0'),
       (1, '2025-04-10 01:00:00', '18.0'),
       (1, '2025-04-11 02:00:00', '18.1'),
       (1, '2025-04-11 03:00:00', '18.0'),
       (1, '2025-04-11 04:00:00', '18.1'),
       (1, '2025-04-11 05:00:00', '18.0'),
       (1, '2025-04-11 06:00:00', '18.1'),
       (1, '2025-04-11 07:00:00', '18.0'),
       (1, '2025-04-11 08:00:00', '18.1'),
       (1, '2025-04-11 09:00:00', '18.1'),
       (1, '2025-04-11 10:00:00', '18.0'),
       (1, '2025-04-11 11:00:00', '18.1'),
       (1, '2025-04-11 12:00:00', '18.3'),
       (1, '2025-04-11 13:00:00', '18.2'),
       (1, '2025-04-11 14:00:00', '18.3'),
       (1, '2025-04-11 15:00:00', '18.1'),
       (1, '2025-04-11 16:00:00', '18.2'),
       (1, '2025-04-11 17:00:00', '18.1'),
       (1, '2025-04-11 18:00:00', '18.2'),
       (1, '2025-04-11 19:00:00', '18.2'),
       (1, '2025-04-11 20:00:00', '18.1'),
       (1, '2025-04-11 21:00:00', '18.1'),
       (1, '2025-04-11 22:00:00', '18.1'),
       (1, '2025-04-11 23:00:00', '18.0');

INSERT INTO parameters_values(parameter_id, time, VALUE)
VALUES (2, '2025-04-10 10:00:00', '18.2'),
       (2, '2025-04-10 11:00:00', '18.2'),
       (2, '2025-04-10 12:00:00', '18.1'),
       (2, '2025-04-10 13:00:00', '18.1'),
       (2, '2025-04-10 14:00:00', '18.2'),
       (2, '2025-04-10 15:00:00', '18.2'),
       (2, '2025-04-10 16:00:00', '18.1'),
       (2, '2025-04-10 17:00:00', '18.1'),
       (2, '2025-04-10 18:00:00', '18.1'),
       (2, '2025-04-10 19:00:00', '18.0'),
       (2, '2025-04-10 20:00:00', '18.1'),
       (2, '2025-04-10 21:00:00', '18.0'),
       (2, '2025-04-10 22:00:00', '18.1'),
       (2, '2025-04-10 23:00:00', '18.1'),
       (2, '2025-04-11 00:00:00', '18.2'),
       (2, '2025-04-10 01:00:00', '18.0'),
       (2, '2025-04-11 02:00:00', '18.1'),
       (2, '2025-04-11 03:00:00', '18.2'),
       (2, '2025-04-11 04:00:00', '18.1'),
       (2, '2025-04-11 05:00:00', '18.0'),
       (2, '2025-04-11 06:00:00', '18.1'),
       (2, '2025-04-11 07:00:00', '18.0'),
       (2, '2025-04-11 08:00:00', '18.0'),
       (2, '2025-04-11 09:00:00', '18.1'),
       (2, '2025-04-11 10:00:00', '18.0'),
       (2, '2025-04-11 11:00:00', '18.1'),
       (2, '2025-04-11 12:00:00', '18.1'),
       (2, '2025-04-11 13:00:00', '18.2'),
       (2, '2025-04-11 14:00:00', '18.3'),
       (2, '2025-04-11 15:00:00', '18.3'),
       (2, '2025-04-11 16:00:00', '18.2'),
       (2, '2025-04-11 17:00:00', '18.1'),
       (2, '2025-04-11 18:00:00', '18.2'),
       (2, '2025-04-11 19:00:00', '18.2'),
       (2, '2025-04-11 20:00:00', '18.3'),
       (2, '2025-04-11 21:00:00', '18.3'),
       (2, '2025-04-11 22:00:00', '18.1'),
       (2, '2025-04-11 23:00:00', '18.2');