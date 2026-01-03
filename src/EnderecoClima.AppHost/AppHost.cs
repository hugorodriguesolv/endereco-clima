var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.EnderecoClima_WebApi>("enderecoclima-webapi");

builder.Build().Run();