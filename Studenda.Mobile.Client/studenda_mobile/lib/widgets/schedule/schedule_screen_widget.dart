import 'package:flutter/material.dart';
import 'package:studenda_mobile/model/schedule/Management/day_schedule.dart';
import 'package:studenda_mobile/model/schedule/subject.dart';
import 'package:studenda_mobile/widgets/schedule/date_carousel_widget.dart';
import 'package:studenda_mobile/widgets/schedule/week_schedule_widget.dart';

final List<DaySchedule> schedule = <DaySchedule>[
  DaySchedule(0, <Subject>[
    Subject(0, "Математика", "ВЦ-315"),
    Subject(1, "Физкультура", "ВЦ-315"),
    Subject(3, "Экономика", "ВЦ-315"),
  ]),
  DaySchedule(1, <Subject>[
    Subject(0, "Математика", "ВЦ-315"),
    Subject(1, "Физкультура", "ВЦ-315"),
    Subject(2, "Базы данных", "ВЦ-315"),
    Subject(3, "Экономика", "ВЦ-315"),
    Subject(2, "Базы данных", "ВЦ-315"),
    Subject(3, "Экономика", "ВЦ-315"),
  ]),
  DaySchedule(2, <Subject>[
    Subject(0, "Математика", "ВЦ-315"),
    Subject(2, "Базы данных", "ВЦ-315"),
    Subject(3, "Экономика", "ВЦ-315"),
  ]),
  DaySchedule(3, <Subject>[
    Subject(0, "Математика", "ВЦ-315"),
    Subject(1, "Физкультура", "ВЦ-315"),
    Subject(2, "Базы данных", "ВЦ-315"),
    Subject(3, "Экономика", "ВЦ-315"),
    Subject(2, "Базы данных", "ВЦ-315"),
    Subject(3, "Экономика", "ВЦ-315"),
  ]),
  DaySchedule(4, <Subject>[
    Subject(0, "Математика", "ВЦ-315"),
    Subject(3, "Экономика", "ВЦ-315"),
  ]),
];

final List<String> dates = <String>[
  "20",
  "21",
  "22",
  "23",
  "24",
  "25",
];

class ScheduleScreenWidget extends StatefulWidget {
  const ScheduleScreenWidget({super.key});

  @override
  State<ScheduleScreenWidget> createState() => _ScheduleScreenWidgetState();
}

class _ScheduleScreenWidgetState extends State<ScheduleScreenWidget> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color.fromARGB(255, 240, 241, 245),
      appBar: AppBar(
        titleSpacing: 0,
        automaticallyImplyLeading: false,
        centerTitle: true,
        title: const Text(
          'Главная',
          style: TextStyle(color: Colors.white, fontSize: 25),
        ),
        actions: [
          IconButton(
            onPressed: () => {Navigator.of(context).pushReplacementNamed('/notification')},
            icon: const Icon(Icons.notifications, color: Colors.white),
          ),
        ],
      ),
      body: Padding(
        padding: const EdgeInsets.all(14.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            const SizedBox(height: 17),
            DateCarouselWidget(dates: dates),
            const SizedBox(height: 10),
            Expanded(
              child: SingleChildScrollView(
                  child: WeekScheduleWidget(schedule: schedule),),
            ),
          ],
        ),
      ),
    );
  }
}
