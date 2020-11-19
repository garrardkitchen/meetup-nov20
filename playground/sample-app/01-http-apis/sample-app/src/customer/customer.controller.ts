import { Controller, Inject, Post, Body } from '@nestjs/common';
import { CustomerService } from './customer.service';

@Controller('customer')
export class CustomerController {

    constructor(private readonly customerService: CustomerService){}

    @Post('add')
    async Add(@Body() customer: any) : Promise<any | null> {
        return await this.customerService.Add(customer)
    }
}
