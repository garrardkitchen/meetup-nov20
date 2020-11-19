import { Injectable, Logger } from '@nestjs/common';
import { InjectConnection } from '@nestjs/mongoose';
import { Connection } from 'mongoose';
import { InjectModel } from '@nestjs/mongoose';
import { Product, ProductDocument } from '../schemas/product.schema';
import { Model } from 'mongoose';

@Injectable()
export class ProductService {
    constructor(private readonly logger: Logger, 
        @InjectModel(Product.name) private productModel: Model<ProductDocument>,
        @InjectConnection('products') private connection: Connection){}

    async Add(productDto: any) : Promise<any | null> {
        this.logger.log(`Adding a new customer`, 'ProductService.Add')
        const createdCat = new this.productModel(productDto);
        createdCat.save()
        return productDto
    }

}
