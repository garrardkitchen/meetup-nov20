import { NestFactory } from '@nestjs/core';
import { Transport, MicroserviceOptions } from '@nestjs/microservices';
import { AppModule } from './app.module';


const getHost = () => {
  return (null == process.env.NATS_SVC_SERVICE_HOST ? 'nats' : process.env.NATS_SVC_SERVICE_HOST)
}

async function bootstrap() {
  const app = await NestFactory.createMicroservice<MicroserviceOptions>(
    AppModule,
    {
      transport: Transport.NATS,
      options: {
        url: `nats://${getHost()}:4222`
      }
    },
  );
  app.listen(() => console.log('Microservice is listening'));
}
bootstrap();
