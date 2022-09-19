using AppModels.DTOs;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;

namespace AppServices.Services
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly ICustomerService _customerService;

        private readonly IMapper _mapper;

        public CustomerAppService(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public long Create(PostCustomerDto postCustomerDto)
        {
            var customerMapeado = _mapper.Map<CustomerModel>(postCustomerDto);
            
            return _customerService.Create(customerMapeado);
        }

        public bool Delete(long id)
        {
            return _customerService.Delete(id);
        }

        public List<GetCustomerDto> GetAll()
        {
            var customers = _customerService.GetAll();

            return _mapper.Map<List<GetCustomerDto>>(customers);
        }

        public GetCustomerDto GetById(long id)
        {
            var customer = _customerService.GetById(id);

            return _mapper.Map<GetCustomerDto>(customer);
        }

        public bool Update(long id, PutCustomerDto putCustomerDto)
        {
            var customerMapeado = _mapper.Map<CustomerModel>(putCustomerDto);

            customerMapeado.Id = id;

            return _customerService.Update(customerMapeado);
        }
    }
}
