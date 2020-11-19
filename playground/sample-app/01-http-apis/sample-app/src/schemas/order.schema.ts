import { Prop, Schema, SchemaFactory } from '@nestjs/mongoose';
import { Document } from 'mongoose';

export type OrderDocument = Order & Document;

@Schema()
export class Order {
  @Prop()
  customerId: string;

  @Prop()
  orderPlaced: string;

  @Prop()
  items: string[];
}

export const OrderSchema = SchemaFactory.createForClass(Order);