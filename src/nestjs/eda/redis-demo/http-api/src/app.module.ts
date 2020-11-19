import { Logger, Module } from '@nestjs/common';
import { ClientsModule, Transport } from '@nestjs/microservices';
import { AppController } from './app.controller';
import { AppService } from './app.service';

const getHost = () => {
  return (null == process.env.REDIS_SVC_SERVICE_HOST ? 'localhost' : process.env.REDIS_SVC_SERVICE_HOST)
}

@Module({
  imports: [
    ClientsModule.register([
      { 
        name: 'MATH_SERVICE', transport: Transport.REDIS, 
        options: {
          url:  `redis://${getHost()}:6379`, 
         },
      }
    ]),
  ],
  controllers: [AppController],
  providers: [AppService, Logger],
})
export class AppModule {}
