import { Controller, Get, Inject, Logger, Query } from '@nestjs/common';
import { ClientProxy } from '@nestjs/microservices';
import { Observable } from 'rxjs';
import { AppService } from './app.service';

@Controller()
export class AppController {
  constructor(private readonly appService: AppService, 
    private readonly logger: Logger,
     @Inject('MATH_SERVICE') private client: ClientProxy) {}

  @Get()
  getSum(@Query('sum') list: string): Observable<number> {
    this.logger.log("GET / hit", "AppController.getSum")
    return this.client.send<number>({cmd: 'sum'}, list.split(',').map(Number))
  }
}
