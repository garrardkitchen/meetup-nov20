import { Logger, Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { Consumer } from "./consumer";
import { BullModule } from "@nestjs/bull";

const getHost = () => {
  return (null == process.env.REDIS_SVC_SERVICE_HOST ? 'localhost' : process.env.REDIS_SVC_SERVICE_HOST)
}

@Module({
  imports: [  BullModule.registerQueue({
    name: 'calc',
    redis: {
      host: getHost(),
      port: 6379,
    },
  })
  ],
  controllers: [AppController],
  providers: [AppService, Consumer, Logger],
})
export class AppModule {}
