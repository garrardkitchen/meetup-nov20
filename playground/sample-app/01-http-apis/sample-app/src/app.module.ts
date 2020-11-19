import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { CustomerController } from './customer/customer.controller';
import { BasketController } from './basket/basket.controller';
import { OrderController } from './order/order.controller';
import { SalesController } from './sales/sales.controller';
import { StockController } from './stock/stock.controller';
import { ProductController } from './product/product.controller';
import { CustomerService } from './customer/customer.service';
import { Logger } from '@nestjs/common';
import { MongooseModule } from '@nestjs/mongoose';
import { Customer, CustomerSchema } from './schemas/customer.schema';
import { Product, ProductSchema } from './schemas/product.schema';
import { Order, OrderSchema } from './schemas/order.schema';
import { ConfigModule } from '@nestjs/config';
import { ProductService } from './product/product.service';
import { OrderService } from './order/order.service';

const getMongoDbUrl = () => {  

    let creds = ""
    // let hostname = "localhost"
    if (null != process.env.MONGODB_USERNAME) {
      creds = `${process.env.MONGODB_USERNAME}:${process.env.MONGODB_PASSWORD}@`      
    }

    // if (null != process.env.MONGODB_DOMAIN) {
    //   hostname = process.env.MONGODB_DOMAIN
    // }

    const url = (null == process.env.MONGODB_SVC_SERVICE_HOST ? `mongodb://${creds}localhost:28017` : `mongodb://${creds}${process.env.MONGODB_SVC_SERVICE_HOST}:${process.env.MONGODB_SVC_SERVICE_PORT}`)  
    console.log(`MONGODB_URL=${url}`);
    
    return url
}

@Module({
  imports: [
    ConfigModule.forRoot(),
    MongooseModule.forFeature([{name: Customer.name, schema: CustomerSchema}], 'customers'),  
    MongooseModule.forFeature([{name: Product.name, schema: ProductSchema}], 'products'),  
    MongooseModule.forFeature([{name: Order.name, schema: OrderSchema}], 'orders'),  
    MongooseModule.forRoot(getMongoDbUrl(), {connectionName: "customers" }),
    MongooseModule.forRoot(getMongoDbUrl(), {connectionName: "products" }),
    MongooseModule.forRoot(getMongoDbUrl(), {connectionName: "orders" })
  ],
  controllers: [AppController, CustomerController, BasketController, OrderController, SalesController, StockController, ProductController],
  providers: [AppService, CustomerService, Logger, ProductService, OrderService],
})
export class AppModule {}
