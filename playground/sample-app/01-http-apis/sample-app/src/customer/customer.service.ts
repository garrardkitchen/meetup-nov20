import { Injectable, Logger } from '@nestjs/common';
import { InjectConnection } from '@nestjs/mongoose';
import { Connection } from 'mongoose';
import { InjectModel } from '@nestjs/mongoose';
import { Customer, CustomerDocument } from '../schemas/customer.schema';
import { Model } from 'mongoose';

@Injectable()
export class CustomerService {

    constructor(private readonly logger: Logger, 
        @InjectModel(Customer.name) private customerModel: Model<CustomerDocument>,
        @InjectConnection('customers') private connection: Connection){}

    async Add(customerDto: any) : Promise<any | null> {
        this.logger.log(`Adding a new customer`, 'CustomerService.Add')
        const createdCat = new this.customerModel(customerDto);
        createdCat.save()
        return customerDto
    }
}
