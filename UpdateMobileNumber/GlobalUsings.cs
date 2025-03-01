﻿global using System.Data;
global using System.Data.SqlClient;
global using System.Globalization;
global using System.Text;
global using CsvHelper;
global using CsvHelper.Configuration;
global using Dapper;
global using ExcelDataReader;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using Newtonsoft.Json;
global using Newtonsoft.Json.Serialization;
global using NLog.Extensions.Logging;
global using NLog.Web;
global using Refit;
global using UpdateMobileNumber.Extensions;
global using UpdateMobileNumber.Infrastructure;
global using UpdateMobileNumber.Infrastructure.Api;
global using UpdateMobileNumber.Infrastructure.Entities;
global using UpdateMobileNumber.Models;
global using UpdateMobileNumber.Models.Enums;
global using UpdateMobileNumber.Options;
global using UpdateMobileNumber.Processors.CSVProcessor;
global using UpdateMobileNumber.Processors.ExcelProcessor;
global using UpdateMobileNumber.Processors.ProcessorFactory;
global using UpdateMobileNumber.Processors.Services;
