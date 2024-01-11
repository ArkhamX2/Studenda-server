import 'package:flutter/material.dart';
import 'package:studenda_mobile/feature/schedule/domain/entities/subject_entity.dart';
import 'package:studenda_mobile/feature/schedule/presentation/widgets/date_carousel_widget.dart';
import 'package:studenda_mobile/feature/schedule/presentation/widgets/week_schedule_widget.dart';
import 'package:studenda_mobile/model/schedule/Management/day_schedule.dart';
import 'package:studenda_mobile/resources/colors.dart';

final List<DaySchedule> schedule = <DaySchedule>[
  DaySchedule(0, <SubjectEntity>[
    SubjectEntity(0, "Математика", "ВЦ-315", "QWERTY"),
    SubjectEntity(1, "Физкультура", "ВЦ-315", "QWERTY"),
    SubjectEntity(3, "Экономика", "ВЦ-315", "QWERTY"),
  ]),
  DaySchedule(1, <SubjectEntity>[
    SubjectEntity(0, "Математика", "ВЦ-315", "QWERTY"),
    SubjectEntity(1, "Физкультура", "ВЦ-315", "QWERTY"),
    SubjectEntity(2, "Базы данных", "ВЦ-315", "QWERTY"),
    SubjectEntity(3, "Экономика", "ВЦ-315", "QWERTY"),
    SubjectEntity(2, "Базы данных", "ВЦ-315", "QWERTY"),
    SubjectEntity(3, "Экономика", "ВЦ-315", "QWERTY"),
  ]),
  DaySchedule(3, <SubjectEntity>[
    SubjectEntity(0, "Математика", "ВЦ-315", "QWERTY"),
    SubjectEntity(1, "Физкультура", "ВЦ-315", "QWERTY"),
    SubjectEntity(2, "Базы данных", "ВЦ-315", "QWERTY"),
    SubjectEntity(3, "Экономика", "ВЦ-315", "QWERTY"),
    SubjectEntity(2, "Базы данных", "ВЦ-315", "QWERTY"),
    SubjectEntity(3, "Экономика", "ВЦ-315", "QWERTY"),
  ]),
  DaySchedule(4, <SubjectEntity>[
    SubjectEntity(0, "Математика", "ВЦ-315", "QWERTY"),
    SubjectEntity(3, "Экономика", "ВЦ-315", "QWERTY"),
  ]),
  DaySchedule(5, <SubjectEntity>[
    SubjectEntity(0, "Математика", "ВЦ-315", "QWERTY"),
    SubjectEntity(2, "Базы данных", "ВЦ-315", "QWERTY"),
    SubjectEntity(3, "Экономика", "ВЦ-315", "QWERTY"),
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

//

class ScheduleScreenWidget extends StatefulWidget {
  const ScheduleScreenWidget({super.key});

  @override
  State<ScheduleScreenWidget> createState() => _ScheduleScreenWidgetState();
}

class _ScheduleScreenWidgetState extends State<ScheduleScreenWidget> {
  static final List<GlobalObjectKey> _key = List.generate(
    schedule.length,
    (index) => GlobalObjectKey(schedule[index].weekPosition),
  );

//TODO: сделать чтобы кнопки влево вправо меняли неделю
//TODO: сделать чтобы вместо "главная бросало на выбор группы"
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
            onPressed: () => {Navigator.of(context).pushNamed('/notification')},
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
            DateCarouselWidget(
              dates: dates,
              onDateTap: (int index) {
                final destination = _key.where((key) => key.value == index);
                if (destination.isNotEmpty) {
                  Scrollable.ensureVisible(
                    destination.first.currentContext!,
                    duration: const Duration(seconds: 1),
                  );
                } else {
                  ScaffoldMessenger.of(context)
                    ..removeCurrentSnackBar()
                    ..showSnackBar(
                      const SnackBar(
                        content: Text(
                          'В этот день занятий нет',
                          style: TextStyle(color: mainForegroundColor),
                        ),
                        backgroundColor: mainButtonBackgroundColor,
                      ),
                    );
                }
              },
              onPrevTap: () => {},
              onNextTap: () => {},
            ),
            const SizedBox(height: 10),
            Expanded(
              child: SingleChildScrollView(
                physics: const BouncingScrollPhysics(),
                child: WeekScheduleWidget(
                  schedule: schedule,
                  keys: _key,
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
