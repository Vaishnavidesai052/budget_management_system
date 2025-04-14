-- MySQL dump 10.13  Distrib 8.0.40, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: budgetsystem_db
-- ------------------------------------------------------
-- Server version	8.0.40

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `tbl_approval`
--

DROP TABLE IF EXISTS `tbl_approval`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_approval` (
  `Req_Id` int NOT NULL AUTO_INCREMENT,
  `Dept_Id` int NOT NULL,
  `User_Id` int NOT NULL,
  `Budget_Estimation` int unsigned DEFAULT NULL,
  `Date` date NOT NULL,
  `Year` int NOT NULL,
  `Status_Id` int NOT NULL,
  `Remark` varchar(1000) DEFAULT NULL,
  `year_id` int DEFAULT NULL,
  PRIMARY KEY (`Req_Id`),
  KEY `Dept_Id` (`Dept_Id`),
  KEY `Status_Id` (`Status_Id`),
  KEY `fk_approval_user` (`User_Id`),
  KEY `fk_year` (`year_id`),
  CONSTRAINT `fk_approval_user` FOREIGN KEY (`User_Id`) REFERENCES `tbl_user` (`user_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_year` FOREIGN KEY (`year_id`) REFERENCES `tbl_financial_years` (`id`),
  CONSTRAINT `tbl_approval_ibfk_1` FOREIGN KEY (`Dept_Id`) REFERENCES `tbl_department` (`department_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `tbl_approval_ibfk_2` FOREIGN KEY (`Status_Id`) REFERENCES `tbl_status` (`statusID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_approval`
--

LOCK TABLES `tbl_approval` WRITE;
/*!40000 ALTER TABLE `tbl_approval` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbl_approval` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-04-13 21:55:58
