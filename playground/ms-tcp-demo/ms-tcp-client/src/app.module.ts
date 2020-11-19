import { Logger, Module } from '@nestjs/common';
import { ClientsModule, Transport } from '@nestjs/microservices';
import { AppController } from './app.controller';
import { AppService } from './app.service';

const getHost = () => {
  return (null == process.env.MATH_SVC_SERVICE_HOST ? 'math' : process.env.MATH_SVC_SERVICE_HOST)
}

@Module({
  imports: [
    ClientsModule.register([
      { 
        name: 'MATH_SERVICE', transport: Transport.TCP,
        options: {port: 3100, host: getHost()} },
    ]),
  ],
  controllers: [AppController],
  providers: [AppService, Logger],
})
export class AppModule {}
