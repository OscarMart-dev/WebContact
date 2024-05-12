CREATE DATABASE `data`;

-- `data`.req_phonebook definition

CREATE TABLE `req_phonebook` (
  `reqc_id` varchar(20) NOT NULL,
  `reqn_name` varchar(50) NOT NULL,
  `reqc_cell_phone` decimal(20,0) NOT NULL,
  `reqn_post` varchar(50) DEFAULT NULL,
  `reqc_office_phone` decimal(20,0) DEFAULT NULL,
  `reqn_picture` longblob,
  PRIMARY KEY (`reqc_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;