import { Controller, Get, Query } from '@nestjs/common';
import { AppService } from './app.service';
import { Job } from "bull";

@Controller()
export class AppController {
  constructor(private readonly appService: AppService) {}

  @Get()
  async getSum(@Query('sum') sum: string): Promise<any> {
    const job = await this.appService.Push( sum.split(',').map(Number));
    return {jobId: job.id};
  }
}
