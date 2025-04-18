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
INSERT INTO physical_values(id, name, designation, description) VALUES (13, 'Время', 'ts', 'Время');
INSERT INTO physical_values(id, name, designation, description) VALUES (14, 'Скорость', 'Vs', 'Скорость');
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
VALUES (8, 'об/мин', 'об/мин', 'Оборотов в минуту', (SELECT id FROM physical_values AS pv WHERE pv.designation = 'Vs'));
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