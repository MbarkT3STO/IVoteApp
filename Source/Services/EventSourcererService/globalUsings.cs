global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading.Tasks;
global using System.Text.Json;


global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using AutoMapper;



global using RabbitMq.Messages.AuthServiceMessages;


global using EventSourcererService.DATA;
global using EventSourcererService.Common;
global using EventSourcererService.DATA.Entities.JsonDataTypes;
// global using EventSourcererService.Enums;

global using EventSourcererService.DATA.Entities.AuthService;
global using EventSourcererService.DATA.Entities.ElectionService;


global using RabbitMq.Settings;
global using RabbitMq.Messages.Abstractions;
global using RabbitMq.Settings.Interfaces;
global using RabbitMq.Settings.QueueRoutes;
