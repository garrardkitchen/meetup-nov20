import { Body, Controller, Post } from '@nestjs/common';
import { OrderService } from './order.service';

@Controller('order')
export class OrderController {
    constructor(private readonly orderService: OrderService){}

    @Post('add')
    async Add(@Body() customer: any) : Promise<any | null> {
        return await this.orderService.Add(customer)
    }

}
