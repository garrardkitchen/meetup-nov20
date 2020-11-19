import {
  CacheInterceptor,
  CacheKey,
  CacheTTL,
  Controller,
  Get,
  Inject,
  Logger,
  Query,
  UseInterceptors
} from '@nestjs/common';
import { ClientProxy } from '@nestjs/microservices';
import { Observable } from 'rxjs';
import { AppService } from './app.service';

@Controller()
export class AppController {
  constructor(private readonly appService: AppService, 
    private readonly logger: Logger,
     @Inject('MATH_SERVICE') private client: ClientProxy) {}

  @Get()
  @UseInterceptors(CacheInterceptor)
  @CacheTTL(5)
  getSum(@Query('sum') list: string): Observable<number> {
    this.logger.log("GET / hit", "AppController.getSum")
    return this.client.send<number>({cmd: 'sum'}, list.split(',').map(Number))
  }

  @Get("random")
  @UseInterceptors(CacheInterceptor)
  @CacheTTL(10)
  @CacheKey('my_custom_key_random')
  getRandom(): number {
    this.logger.log("GET / hit", "AppController.getRandom")
    return Math.random()
  }
}
