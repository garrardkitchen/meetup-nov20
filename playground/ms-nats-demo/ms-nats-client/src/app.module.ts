import { Logger, Module } from '@nestjs/common';
import { ClientsModule, Transport } from '@nestjs/microservices';
import { AppController } from './app.controller';
import { AppService } from './app.service';

const getHost = () => {
  return (null == process.env.NATS_SVC_SERVICE_HOST ? 'nats' : process.env.NATS_SVC_SERVICE_HOST)
}

@Module({
  imports: [
    ClientsModule.register([
      { 
        name: 'MATH_SERVICE', transport: Transport.NATS, 
        options: {
          url:  `nats://${getHost()}:4222`, 
         },
      }
    ]),
  ],
  controllers: [AppController],
  providers: [AppService, Logger],
})
export class AppModule {}
