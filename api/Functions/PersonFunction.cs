using System;
using System.Net;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using System.Collections.Generic;
using AutoMapper;

namespace StaticWebApp.Template
{
    public class PersonFunction
    {
        private readonly ILoggerHelper _loggerHelper;
        private readonly ITableStorageRepository _tableStorageRepository;
        private readonly IMapper _mapper;
        public PersonFunction(
            IMapper mapper,
            ILoggerHelper loggerHelper,
            ITableStorageRepository tableStorageRepository)
        {
            _loggerHelper = loggerHelper;
            _tableStorageRepository = tableStorageRepository;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(PersonDTO), (int)HttpStatusCode.Created)]
        [FunctionName("create-person")]
        public async Task<IActionResult> CreatePerson(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/person/create")]
            [RequestBodyType(typeof(PersonDTO), "model")]
            HttpRequest httpRequest)
        {
            if (httpRequest.Method.Equals("post", StringComparison.OrdinalIgnoreCase))
            {
                using (var reader = new StreamReader(httpRequest.Body))
                {
                    var json = await reader.ReadToEndAsync();
                    var dto = JsonConvert.DeserializeObject<PersonDTO>(json);
                    var person = _mapper.Map<Person>(dto);
                    if (string.IsNullOrEmpty(person.RowKey))
                    {
                        person.RowKey = Guid.NewGuid().ToString();
                    }

                    var p = await _tableStorageRepository.InsertOrMergeEntityAsync<Person>(person);

                    return new CreatedResult("", _mapper.Map<PersonDTO>(p));
                }
            }

            return new OkResult();
        }

        [ProducesResponseType(typeof(IEnumerable<PersonDTO>), (int)HttpStatusCode.OK)]
        [FunctionName("get-all-persons")]
        public async Task<IActionResult> GetAllPersons(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/person/get-all")]
            HttpRequest httpRequest)
        {
            var persons = await _tableStorageRepository.GetAllEntityRows<Person>();
            return new ObjectResult(persons.Select(x => _mapper.Map<PersonDTO>(x)));
        }
    }
}