-- MySqlBackup.NET 2.0.9.2
-- Dump Time: 2017-11-19 23:29:22
-- --------------------------------------
-- Server version 5.5.55 MySQL Community Server (GPL)


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES latin1 */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- 
-- Definition of areas
-- 

DROP TABLE IF EXISTS `areas`;
CREATE TABLE IF NOT EXISTS `areas` (
  `id_area` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `nombre_area` varchar(100) NOT NULL,
  `nombre_contacto` varchar(100) NOT NULL,
  `correo_contacto` varchar(255) NOT NULL,
  `tlfn_contacto` varchar(11) NOT NULL,
  `id_cliente1` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id_area`),
  KEY `id_clientes1_idx` (`id_cliente1`),
  CONSTRAINT `id_cliente1` FOREIGN KEY (`id_cliente1`) REFERENCES `clientes` (`id_clientes`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;

-- 
-- Dumping data for table areas
-- 

/*!40000 ALTER TABLE `areas` DISABLE KEYS */;
INSERT INTO `areas`(`id_area`,`nombre_area`,`nombre_contacto`,`correo_contacto`,`tlfn_contacto`,`id_cliente1`) VALUES
(5,'Talento humano','Pedro mata','RATTANPEREZ@HOTMAIL.COM','02128901212',3),
(6,'Operaciones','Maria jimenez','jimenez.maria@gmail.com','04147890327',3),
(7,'RRHH','Marianne','Marianne.perez@outlook.com','04126557890',4);
/*!40000 ALTER TABLE `areas` ENABLE KEYS */;

-- 
-- Definition of aulas
-- 

DROP TABLE IF EXISTS `aulas`;
CREATE TABLE IF NOT EXISTS `aulas` (
  `id_aula` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `aula_re` varchar(45) NOT NULL,
  PRIMARY KEY (`id_aula`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- 
-- Dumping data for table aulas
-- 

/*!40000 ALTER TABLE `aulas` DISABLE KEYS */;

/*!40000 ALTER TABLE `aulas` ENABLE KEYS */;

-- 
-- Definition of clientes
-- 

DROP TABLE IF EXISTS `clientes`;
CREATE TABLE IF NOT EXISTS `clientes` (
  `id_clientes` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `nombre_empresa` varchar(100) NOT NULL,
  `fee_empresa` int(1) NOT NULL,
  `ci_user1` int(8) unsigned NOT NULL,
  PRIMARY KEY (`id_clientes`),
  KEY `user_reg_cliente_idx` (`ci_user1`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- 
-- Dumping data for table clientes
-- 

/*!40000 ALTER TABLE `clientes` DISABLE KEYS */;
INSERT INTO `clientes`(`id_clientes`,`nombre_empresa`,`fee_empresa`,`ci_user1`) VALUES
(3,'Rattan ca',0,0),
(4,'Norkut',1,0);
/*!40000 ALTER TABLE `clientes` ENABLE KEYS */;

-- 
-- Definition of clientes_solicitan_cursos
-- 

DROP TABLE IF EXISTS `clientes_solicitan_cursos`;
CREATE TABLE IF NOT EXISTS `clientes_solicitan_cursos` (
  `id_cliente1` int(11) unsigned NOT NULL,
  `id_curso1` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id_cliente1`,`id_curso1`),
  KEY `curso_existente_idx` (`id_curso1`),
  CONSTRAINT `cliente_solicita` FOREIGN KEY (`id_cliente1`) REFERENCES `clientes` (`id_clientes`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `curso_existente` FOREIGN KEY (`id_curso1`) REFERENCES `cursos` (`id_cursos`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- 
-- Dumping data for table clientes_solicitan_cursos
-- 

/*!40000 ALTER TABLE `clientes_solicitan_cursos` DISABLE KEYS */;

/*!40000 ALTER TABLE `clientes_solicitan_cursos` ENABLE KEYS */;

-- 
-- Definition of cursos
-- 

DROP TABLE IF EXISTS `cursos`;
CREATE TABLE IF NOT EXISTS `cursos` (
  `id_cursos` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `estatus_curso` varchar(45) NOT NULL,
  `tipo_curso` varchar(45) NOT NULL,
  `duracion_curso` varchar(3) NOT NULL,
  `nombre_curso` varchar(45) NOT NULL,
  `fecha_creacion` datetime NOT NULL,
  `id_usuario1` int(8) NOT NULL,
  `id_p_inst` int(11) unsigned NOT NULL,
  `bloque_curso` varchar(1) NOT NULL,
  `solicitud_curso` varchar(70) NOT NULL,
  `id_ref1` int(11) unsigned DEFAULT NULL,
  `etapa_curso` int(1) NOT NULL,
  `fecha_uno` date DEFAULT NULL,
  `fecha_dos` date DEFAULT NULL,
  `horario_uno` varchar(100) DEFAULT NULL,
  `horario_dos` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id_cursos`),
  KEY `cursos_paquete_idx` (`id_p_inst`),
  KEY `cursos_ref_idx` (`id_ref1`),
  KEY `cursos_user_idx` (`id_usuario1`),
  CONSTRAINT `cursos_pq` FOREIGN KEY (`id_p_inst`) REFERENCES `p_instruccional` (`id_pinstruccional`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `cursos_ref` FOREIGN KEY (`id_ref1`) REFERENCES `refrigerios` (`id_ref`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `cursos_user` FOREIGN KEY (`id_usuario1`) REFERENCES `usuarios` (`id_user`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=latin1;

-- 
-- Dumping data for table cursos
-- 

/*!40000 ALTER TABLE `cursos` DISABLE KEYS */;
INSERT INTO `cursos`(`id_cursos`,`estatus_curso`,`tipo_curso`,`duracion_curso`,`nombre_curso`,`fecha_creacion`,`id_usuario1`,`id_p_inst`,`bloque_curso`,`solicitud_curso`,`id_ref1`,`etapa_curso`,`fecha_uno`,`fecha_dos`,`horario_uno`,`horario_dos`) VALUES
(1,'Suspendido','Abierto','4','Formacion uno','2017-11-11 19:38:41',1,4,'1','karla ',NULL,1,NULL,NULL,NULL,NULL),
(2,'Finalizado','Abierto','4','Formacion dos','2017-11-12 21:41:10',1,5,'1','zoyla',NULL,1,NULL,NULL,NULL,NULL),
(3,'En curso','Abierto','8','Formacion tres','2017-11-13 19:11:15',1,6,'2','yo',NULL,1,NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `cursos` ENABLE KEYS */;

-- 
-- Definition of cursos_has_difusion
-- 

DROP TABLE IF EXISTS `cursos_has_difusion`;
CREATE TABLE IF NOT EXISTS `cursos_has_difusion` (
  `cursos_id_cursos` int(11) unsigned NOT NULL,
  `difusion_id_difusion` int(11) unsigned NOT NULL,
  PRIMARY KEY (`cursos_id_cursos`,`difusion_id_difusion`),
  KEY `fk_cursos_has_difusion_difusion1_idx` (`difusion_id_difusion`),
  KEY `fk_cursos_has_difusion_cursos1_idx` (`cursos_id_cursos`),
  CONSTRAINT `fk_cursos_has_difusion_cursos1` FOREIGN KEY (`cursos_id_cursos`) REFERENCES `cursos` (`id_cursos`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_cursos_has_difusion_difusion1` FOREIGN KEY (`difusion_id_difusion`) REFERENCES `difusion` (`id_difusion`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- 
-- Dumping data for table cursos_has_difusion
-- 

/*!40000 ALTER TABLE `cursos_has_difusion` DISABLE KEYS */;

/*!40000 ALTER TABLE `cursos_has_difusion` ENABLE KEYS */;

-- 
-- Definition of cursos_has_participantes
-- 

DROP TABLE IF EXISTS `cursos_has_participantes`;
CREATE TABLE IF NOT EXISTS `cursos_has_participantes` (
  `cursos_id_cursos` int(11) unsigned NOT NULL,
  `participantes_cedula_par` int(11) NOT NULL,
  PRIMARY KEY (`cursos_id_cursos`,`participantes_cedula_par`),
  KEY `fk_cursos_has_participantes_participantes1_idx` (`participantes_cedula_par`),
  KEY `fk_cursos_has_participantes_cursos1_idx` (`cursos_id_cursos`),
  CONSTRAINT `fk_cursos_has_participantes_cursos1` FOREIGN KEY (`cursos_id_cursos`) REFERENCES `cursos` (`id_cursos`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_cursos_has_participantes_participantes1` FOREIGN KEY (`participantes_cedula_par`) REFERENCES `participantes` (`cedula_par`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- 
-- Dumping data for table cursos_has_participantes
-- 

/*!40000 ALTER TABLE `cursos_has_participantes` DISABLE KEYS */;

/*!40000 ALTER TABLE `cursos_has_participantes` ENABLE KEYS */;

-- 
-- Definition of cursos_inces
-- 

DROP TABLE IF EXISTS `cursos_inces`;
CREATE TABLE IF NOT EXISTS `cursos_inces` (
  `id_curso_ince` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `nombre_curso_ince` varchar(100) NOT NULL,
  PRIMARY KEY (`id_curso_ince`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=latin1;

-- 
-- Dumping data for table cursos_inces
-- 

/*!40000 ALTER TABLE `cursos_inces` DISABLE KEYS */;
INSERT INTO `cursos_inces`(`id_curso_ince`,`nombre_curso_ince`) VALUES
(17,'Manejo de alimentos');
/*!40000 ALTER TABLE `cursos_inces` ENABLE KEYS */;

-- 
-- Definition of cursos_tienen_aulas
-- 

DROP TABLE IF EXISTS `cursos_tienen_aulas`;
CREATE TABLE IF NOT EXISTS `cursos_tienen_aulas` (
  `id_cta_aula` int(11) unsigned NOT NULL,
  `id_cta_curso` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id_cta_aula`,`id_cta_curso`),
  KEY `cta_curso_idx` (`id_cta_curso`),
  CONSTRAINT `cta_aula` FOREIGN KEY (`id_cta_aula`) REFERENCES `aulas` (`id_aula`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `cta_curso` FOREIGN KEY (`id_cta_curso`) REFERENCES `cursos` (`id_cursos`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- 
-- Dumping data for table cursos_tienen_aulas
-- 

/*!40000 ALTER TABLE `cursos_tienen_aulas` DISABLE KEYS */;

/*!40000 ALTER TABLE `cursos_tienen_aulas` ENABLE KEYS */;

-- 
-- Definition of cursos_tienen_fa
-- 

DROP TABLE IF EXISTS `cursos_tienen_fa`;
CREATE TABLE IF NOT EXISTS `cursos_tienen_fa` (
  `cursos_id_cursos` int(11) unsigned NOT NULL,
  `facilitadores_id_fa` int(20) unsigned NOT NULL,
  `ctf_id_cofa` int(11) unsigned DEFAULT NULL,
  PRIMARY KEY (`cursos_id_cursos`,`facilitadores_id_fa`),
  KEY `fk_cursos_has_facilitadores_facilitadores1_idx` (`facilitadores_id_fa`),
  KEY `fk_cursos_has_facilitadores_cursos1_idx` (`cursos_id_cursos`),
  CONSTRAINT `fk_cursos_has_facilitadores_cursos1` FOREIGN KEY (`cursos_id_cursos`) REFERENCES `cursos` (`id_cursos`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_cursos_has_facilitadores_facilitadores1` FOREIGN KEY (`facilitadores_id_fa`) REFERENCES `facilitadores` (`id_fa`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- 
-- Dumping data for table cursos_tienen_fa
-- 

/*!40000 ALTER TABLE `cursos_tienen_fa` DISABLE KEYS */;
INSERT INTO `cursos_tienen_fa`(`cursos_id_cursos`,`facilitadores_id_fa`,`ctf_id_cofa`) VALUES
(1,8,NULL),
(2,2,NULL);
/*!40000 ALTER TABLE `cursos_tienen_fa` ENABLE KEYS */;

-- 
-- Definition of cursos_tienen_insumos
-- 

DROP TABLE IF EXISTS `cursos_tienen_insumos`;
CREATE TABLE IF NOT EXISTS `cursos_tienen_insumos` (
  `cursos_id_cursos` int(11) unsigned NOT NULL,
  `insumos_id_insumos` int(11) NOT NULL,
  PRIMARY KEY (`cursos_id_cursos`,`insumos_id_insumos`),
  KEY `fk_cursos_has_insumos_insumos1_idx` (`insumos_id_insumos`),
  KEY `fk_cursos_has_insumos_cursos1_idx` (`cursos_id_cursos`),
  CONSTRAINT `fk_cursos_has_insumos_cursos1` FOREIGN KEY (`cursos_id_cursos`) REFERENCES `cursos` (`id_cursos`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_cursos_has_insumos_insumos1` FOREIGN KEY (`insumos_id_insumos`) REFERENCES `insumos` (`id_insumos`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- 
-- Dumping data for table cursos_tienen_insumos
-- 

/*!40000 ALTER TABLE `cursos_tienen_insumos` DISABLE KEYS */;

/*!40000 ALTER TABLE `cursos_tienen_insumos` ENABLE KEYS */;

-- 
-- Definition of cursos_tienen_refrigerios
-- 

DROP TABLE IF EXISTS `cursos_tienen_refrigerios`;
CREATE TABLE IF NOT EXISTS `cursos_tienen_refrigerios` (
  `cursos_id_cursos` int(11) unsigned NOT NULL,
  `refrigerios_id_ref` int(11) unsigned NOT NULL,
  PRIMARY KEY (`cursos_id_cursos`,`refrigerios_id_ref`),
  KEY `fk_cursos_has_refrigerios_refrigerios1_idx` (`refrigerios_id_ref`),
  KEY `fk_cursos_has_refrigerios_cursos1_idx` (`cursos_id_cursos`),
  CONSTRAINT `fk_cursos_has_refrigerios_cursos1` FOREIGN KEY (`cursos_id_cursos`) REFERENCES `cursos` (`id_cursos`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_cursos_has_refrigerios_refrigerios1` FOREIGN KEY (`refrigerios_id_ref`) REFERENCES `refrigerios` (`id_ref`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- 
-- Dumping data for table cursos_tienen_refrigerios
-- 

/*!40000 ALTER TABLE `cursos_tienen_refrigerios` DISABLE KEYS */;

/*!40000 ALTER TABLE `cursos_tienen_refrigerios` ENABLE KEYS */;

-- 
-- Definition of difusion
-- 

DROP TABLE IF EXISTS `difusion`;
CREATE TABLE IF NOT EXISTS `difusion` (
  `id_difusion` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `dif_contenido` varchar(200) NOT NULL,
  PRIMARY KEY (`id_difusion`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

-- 
-- Dumping data for table difusion
-- 

/*!40000 ALTER TABLE `difusion` DISABLE KEYS */;
INSERT INTO `difusion`(`id_difusion`,`dif_contenido`) VALUES
(2,'Publicidad por periodico'),
(4,'Publicidad por costazul'),
(5,'EnvÃ­o de correo electrÃ³nico masivo');
/*!40000 ALTER TABLE `difusion` ENABLE KEYS */;

-- 
-- Definition of facilitadores
-- 

DROP TABLE IF EXISTS `facilitadores`;
CREATE TABLE IF NOT EXISTS `facilitadores` (
  `id_fa` int(11) unsigned NOT NULL,
  `cedula_fa` varchar(8) NOT NULL,
  `nacionalidad_fa` varchar(1) NOT NULL,
  `nombre_fa` varchar(45) NOT NULL,
  `apellido_fa` varchar(45) NOT NULL,
  `tlfn_fa` varchar(11) NOT NULL,
  `correo_fa` varchar(75) NOT NULL,
  `ubicacion_fa` varchar(150) NOT NULL,
  `especialidad_fa` varchar(45) NOT NULL,
  `requerimiento_inces` int(1) NOT NULL,
  PRIMARY KEY (`id_fa`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- 
-- Dumping data for table facilitadores
-- 

/*!40000 ALTER TABLE `facilitadores` DISABLE KEYS */;
INSERT INTO `facilitadores`(`id_fa`,`cedula_fa`,`nacionalidad_fa`,`nombre_fa`,`apellido_fa`,`tlfn_fa`,`correo_fa`,`ubicacion_fa`,`especialidad_fa`,`requerimiento_inces`) VALUES
(0,'82546547','E','Maria','Perez','04455555555','hhhh@jfkjj.com','Sucre','Hjjhgghgjhg',1),
(2,'87655432','E','Carlos','Bermudez','04121234567','bermudez@gmail.com','Lara','coach',1),
(3,'24437292','V','Pedro','Perez','04247777777','pedroperez@gmail.com','Sucre','Auditor',0),
(7,'24437205','V','Diego','Molina','04261111111','correo@ejemplo.com','Portuguesa','Tutor',0),
(8,'23590781','V','Sonia','Cabera','02953490549','sonia@gmail.com','Nueva Esparta','Manejo ira',1);
/*!40000 ALTER TABLE `facilitadores` ENABLE KEYS */;

-- 
-- Definition of formatos
-- 

DROP TABLE IF EXISTS `formatos`;
CREATE TABLE IF NOT EXISTS `formatos` (
  `id_formato` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `nombre_archivo` varchar(100) NOT NULL,
  `ruta_archivo` varchar(1000) NOT NULL,
  PRIMARY KEY (`id_formato`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;

-- 
-- Dumping data for table formatos
-- 

/*!40000 ALTER TABLE `formatos` DISABLE KEYS */;
INSERT INTO `formatos`(`id_formato`,`nombre_archivo`,`ruta_archivo`) VALUES
(9,'Encuestas satisfaccion cliente.pdf','C:/Users/ZM/Documents/UNIMAR/Encuestas satisfaccion cliente.pdf'),
(10,'Encuestas satisfaccion participante.pdf','C:/Users/ZM/Documents/UNIMAR/Encuestas satisfaccion participante.pdf');
/*!40000 ALTER TABLE `formatos` ENABLE KEYS */;

-- 
-- Definition of inces_tiene_facilitadores
-- 

DROP TABLE IF EXISTS `inces_tiene_facilitadores`;
CREATE TABLE IF NOT EXISTS `inces_tiene_facilitadores` (
  `id_fa_INCE` int(20) unsigned NOT NULL,
  `id_curso_INCE` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id_fa_INCE`,`id_curso_INCE`),
  KEY `id_curso_inces_idx` (`id_curso_INCE`),
  CONSTRAINT `id_curso_inces` FOREIGN KEY (`id_curso_INCE`) REFERENCES `cursos_inces` (`id_curso_ince`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `id_fa_inces1` FOREIGN KEY (`id_fa_INCE`) REFERENCES `facilitadores` (`id_fa`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- 
-- Dumping data for table inces_tiene_facilitadores
-- 

/*!40000 ALTER TABLE `inces_tiene_facilitadores` DISABLE KEYS */;
INSERT INTO `inces_tiene_facilitadores`(`id_fa_INCE`,`id_curso_INCE`) VALUES
(0,17),
(2,17);
/*!40000 ALTER TABLE `inces_tiene_facilitadores` ENABLE KEYS */;

-- 
-- Definition of insumos
-- 

DROP TABLE IF EXISTS `insumos`;
CREATE TABLE IF NOT EXISTS `insumos` (
  `id_insumos` int(11) NOT NULL AUTO_INCREMENT,
  `ins_contenido` varchar(45) NOT NULL,
  PRIMARY KEY (`id_insumos`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- 
-- Dumping data for table insumos
-- 

/*!40000 ALTER TABLE `insumos` DISABLE KEYS */;
INSERT INTO `insumos`(`id_insumos`,`ins_contenido`) VALUES
(1,'BolÃ­grafo'),
(2,'LÃ¡pices'),
(3,'Rotafolios'),
(4,'Marcadores negros');
/*!40000 ALTER TABLE `insumos` ENABLE KEYS */;

-- 
-- Definition of p_instruccional
-- 

DROP TABLE IF EXISTS `p_instruccional`;
CREATE TABLE IF NOT EXISTS `p_instruccional` (
  `id_pinstruccional` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `p_bitacora` varchar(1000) DEFAULT NULL,
  `p_presentacion` varchar(1000) DEFAULT NULL,
  `p_manual` varchar(1000) DEFAULT NULL,
  `p_contenido` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`id_pinstruccional`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=latin1 COMMENT='Aqui se guardarÃÂ¡n los planes instruccionales \nel contenido de cada formacion como bitacora, presentacion, otros	';

-- 
-- Dumping data for table p_instruccional
-- 

