import { Injectable, Logger } from '@nestjs/common';
import { InjectConnection } from '@nestjs/mongoose';
import { Connection } from 'mongoose';
import { InjectModel } from '@nestjs/mongoose';
import { Order, OrderDocument } from '../schemas/order.schema';
import { Model } from 'mongoose';

@Injectable()
export class OrderService {
    constructor(private readonly logger: Logger, 
        @InjectModel(Order.name) private orderModel: Model<OrderDocument>,
        @InjectConnection('orders') private connection: Connection){}

    async Add(orderDto: any) : Promise<any | null> {
        this.logger.log(`Adding a new customer`, 'OrderService.Add')
        const createdCat = new this.orderModel(orderDto);
        createdCat.save()
        return orderDto
    }


}
