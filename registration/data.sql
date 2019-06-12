CREATE DATABASE `users` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
CREATE TABLE `usersdata` (
  `login` varchar(45) DEFAULT NULL,
  `password` varchar(50) DEFAULT NULL,
  `alphabetCountSuccesses` int(11) DEFAULT '0',
  `figuresCountSuccesses` int(11) DEFAULT '0',
  `numbersCountSuccesses` int(11) DEFAULT '0',
  UNIQUE KEY `login_UNIQUE` (`login`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
