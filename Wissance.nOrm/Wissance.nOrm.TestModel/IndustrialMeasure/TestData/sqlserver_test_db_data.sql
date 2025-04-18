SET IDENTITY_INSERT physical_values ON;
INSERT INTO physical_values(id, name, designation, description) VALUES (1, N'Температура', N'T', N'Температура');
INSERT INTO physical_values(id, name, designation, description) VALUES (2, N'Давление', N'P', N'Давление');
INSERT INTO physical_values(id, name, designation, description) VALUES (3, N'Объем', N'V', N'Объем');
INSERT INTO physical_values(id, name, designation, description) VALUES (4, N'Сила тока', N'I', N'Сила тока');
INSERT INTO physical_values(id, name, designation, description) VALUES (5, N'Электрическое напряжение', N'U', N'Электрическое напряжение');
INSERT INTO physical_values(id, name, designation, description) VALUES (6, N'Масса', N'm', N'Масса');
INSERT INTO physical_values(id, name, designation, description) VALUES (7, N'Частота', N'f', N'Частота');
INSERT INTO physical_values(id, name, designation, description) VALUES (8, N'Плотность', N'ρ', N'Плотность');
INSERT INTO physical_values(id, name, designation, description) VALUES (9, N'Длина', N'L', N'Длина');
INSERT INTO physical_values(id, name, designation, description) VALUES (10, N'Электрическое сопротивление', N'R', N'Электрическое сопротивление');
INSERT INTO physical_values(id, name, designation, description) VALUES (11, N'Вязкость', N'υ', N'Вязкость');
INSERT INTO physical_values(id, name, designation, description) VALUES (12, N'Теплоемкость', N'C', N'Теплоемкость');
INSERT INTO physical_values(id, name, designation, description) VALUES (13, N'Время', N'ts', N'Время');
INSERT INTO physical_values(id, name, designation, description) VALUES (14, N'Скорость', N'Vs', N'Скорость');
INSERT INTO physical_values(id, name, designation, description) VALUES (15, N'Ускорение', N'a', N'Ускорение');
SET IDENTITY_INSERT physical_values OFF;

SET IDENTITY_INSERT measure_units ON;
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (1, N'МПа', N'МПа', N'Мегапаскаль', (SELECT id FROM physical_values AS pv WHERE pv.designation = N'P'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (2, N'Куб.м', N'м3', N'Кубический метр', (SELECT id FROM physical_values AS pv WHERE pv.designation = N'V'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (3, N'Гц', N'Гц', N'Герц', (SELECT id FROM physical_values AS pv WHERE pv.designation = N'f'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (4, N'%', N'%', N'Процент', NULL);
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (5, N'ºC', N'ºC', N'Градус цельсия', (SELECT id FROM physical_values AS pv WHERE pv.designation = N'T'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (6, N'В', N'В', N'Вольт', (SELECT id FROM physical_values AS pv WHERE pv.designation = N'U'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (7, N'г/куб.см', N'г/см3', N'Грам на кубический сантиметр', (SELECT id FROM physical_values AS pv WHERE pv.designation = N'ρ'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (8, N'об/мин', N'об/мин', N'Оборотов в минуту', (SELECT id FROM physical_values AS pv WHERE pv.designation = N'Vs'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (9, N'А', N'А', N'Ампер', (SELECT id FROM physical_values AS pv WHERE pv.designation = N'I'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (10, N'В', N'В', N'Вольт', (SELECT id FROM physical_values AS pv WHERE pv.designation = N'U'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (11, N'м', N'м', N'Метр', (SELECT id FROM physical_values AS pv WHERE pv.designation = N'L'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (12, N'мм', N'мм', N'Миллиметр', (SELECT id FROM physical_values AS pv WHERE pv.designation = N'L'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (13, N'т/сут', N'т/сут', N'Тонн в сутки', NULL);
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (14, N'куб.м/сут', N'м3/сут', N'Кубических метров в сутки', NULL);
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (15, N'млн. куб.м/сут', '', N'Миллионов кубических метров в сутки', NULL);
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (16, N'м/с2', N'м/с2', N'Метр/секунду в квадрате', (SELECT id FROM physical_values AS pv WHERE pv.designation = N'a'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (17, N'МОм', N'МОм', N'Мегаомм', (SELECT id FROM physical_values AS pv WHERE pv.designation = N'R'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (18, N'мм2', N'мм2', N'Квадратный миллиметр', NULL);
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (19, N'об', N'об', N'Оборот', NULL);
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (20, N'мПа/с', N'мПа/с', N'Миллипаскаль в секунду', NULL);
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (21, N'м3/(сут*МПа)', '', N'Кубических метров на мегапаскаль в сутки', NULL);
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (22, '', '', N'Безразмерная величина', NULL);
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (23, N'кг/куб.м', N'кг/куб.м', N'Килограм на кубический метр', (SELECT id FROM physical_values AS pv WHERE pv.designation = 'ρ'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (24, N'Па*с', N'Па*с', N'Паскаль*секунда', (SELECT id FROM physical_values AS pv WHERE pv.designation = N'υ'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (25, N'м3/т', N'м3/т', N'Кубических метров в тонну', null);
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (26, N'кДж/(кг*ºC)', '', N'Килоджоуль на килограм и градус', (SELECT id FROM physical_values AS pv WHERE pv.designation = N'C'));
INSERT INTO measure_units(id, name, designation, description, physical_value_id)
VALUES (27, N'час', N'час', N'Час', (SELECT id FROM physical_values AS pv WHERE pv.designation = N't'));
SET IDENTITY_INSERT measure_units OFF;