import { Controller, Get, Inject, Logger } from '@nestjs/common';
import { ClientProxy } from '@nestjs/microservices';
import { Observable } from 'rxjs';
import { AppService } from './app.service';

@Controller()
export class AppController {
  constructor(private readonly appService: AppService, 
    private readonly logger: Logger,
     @Inject('MATH_SERVICE') private client: ClientProxy) {}

  @Get()
  getHello(): Observable<number> {  
    this.logger.log(`GET / hit ${process.env.MATH_SVC_SERVICE_HOST}`, "AppController.getHello")  
    return this.client.send<number>({cmd: 'sum'}, [1,2,3,4])    
  }
}
