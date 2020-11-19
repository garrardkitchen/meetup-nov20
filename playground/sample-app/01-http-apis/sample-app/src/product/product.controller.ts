import { Body, Controller, Post } from '@nestjs/common';
import { ProductService } from './product.service';

@Controller('product')
export class ProductController {
    constructor(private readonly productService: ProductService){}

    @Post('add')
    async Add(@Body() customer: any) : Promise<any | null> {
        return await this.productService.Add(customer)
    }

}
