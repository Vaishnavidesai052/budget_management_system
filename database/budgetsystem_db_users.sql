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
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `UserID` int NOT NULL AUTO_INCREMENT,
  `Username` varchar(255) DEFAULT NULL,
  `PasswordHash` varchar(255) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `RoleID` int DEFAULT NULL,
  `DepartmentID` int DEFAULT NULL,
  `Status` enum('Active','Inactive') NOT NULL DEFAULT 'Active',
  `CreatedAt` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedAt` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `ResetToken` varchar(255) DEFAULT NULL,
  `ResetTokenExpiration` datetime DEFAULT NULL,
  PRIMARY KEY (`UserID`),
  UNIQUE KEY `Email` (`Email`),
  KEY `RoleID` (`RoleID`),
  KEY `DepartmentID` (`DepartmentID`),
  CONSTRAINT `users_ibfk_1` FOREIGN KEY (`RoleID`) REFERENCES `role` (`RoleID`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `users_ibfk_2` FOREIGN KEY (`DepartmentID`) REFERENCES `department` (`DepartmentID`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (2,'simran','xsttdTosrqgNYZGWQINVtN0jHOFPQf8ic+aGIhmwvFc=','simran07@gmail.com',5,1,'Active','2025-02-03 10:17:34','2025-02-11 12:21:49',NULL,NULL),(3,'Suraj','ur3kjt2Xtsq9SjeNZVydAjsovC7HiAtaPmmrPyfjoCg=','suraj@gmail.com',3,1,'Active','2025-02-10 10:46:41','2025-02-10 10:46:41',NULL,NULL),(8,'vishal','K5j7aVQ2j/PyjzNntpfBrjs9Hmm4Kl3pK1il5AmTu+A=','vishal@gmail.com',4,1,'Active','2025-02-11 11:54:38','2025-02-18 10:24:42',NULL,NULL),(9,'vaishnavi','dPW2i4FPhpzJVKuCGKvD/eDcdKuLJScmjLYQq7Gv428=','vaishnavi@gmail.com',6,1,'Active','2025-02-11 11:59:44','2025-02-24 12:04:38',NULL,NULL),(11,'simran','A6xnQhbz4Vx2HuGl4lXwZ5U2I8iziLRFnhP5eNfIRvQ=','bargirsimran@gmail.com',1,1,'Active','2025-03-04 15:02:30','2025-03-04 15:02:30',NULL,NULL),(12,'vaishnavi','A6xnQhbz4Vx2HuGl4lXwZ5U2I8iziLRFnhP5eNfIRvQ=','vaishnavi12@gmail.com',6,1,'Active','2025-03-18 09:42:07','2025-03-18 09:42:07',NULL,NULL),(13,'MANGESH MARBATE','A6xnQhbz4Vx2HuGl4lXwZ5U2I8iziLRFnhP5eNfIRvQ=','mangesh.marbate@mahametro.org',3,1,'Active','2025-03-20 06:20:29','2025-03-20 06:20:29',NULL,NULL),(14,'ABHIJEET BHOSALE','A6xnQhbz4Vx2HuGl4lXwZ5U2I8iziLRFnhP5eNfIRvQ=','abhijeet.bhosale@mahametro.org',3,1,'Active','2025-03-20 10:55:59','2025-03-20 10:55:59',NULL,NULL),(15,'ABHISHEK THORAIT','A6xnQhbz4Vx2HuGl4lXwZ5U2I8iziLRFnhP5eNfIRvQ=','abhishek.thorait@mahametro.org',3,1,'Active','2025-03-25 05:34:47','2025-03-25 05:34:47',NULL,NULL);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-04-13 21:55:57
