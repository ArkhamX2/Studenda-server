import 'package:flutter/material.dart';
import 'package:studenda_mobile/model/common/notification.dart';
import 'package:studenda_mobile/model/schedule/subject.dart';
import 'package:studenda_mobile/resourses/colors.dart';
import 'package:studenda_mobile/widgets/notification/notification_consts.dart';
import 'package:studenda_mobile/widgets/notification/notification_list_widget.dart';
import 'package:studenda_mobile/widgets/schedule/day_schedule_widget.dart';

final List<Subject> schedule = <Subject>[
  Subject(0, "Математика", "ВЦ-315"),
  Subject(1, "Физкультура", "ВЦ-315"),
  Subject(2, "Базы данных", "ВЦ-315"),
  Subject(3, "Экономика", "ВЦ-315"),
];

final List<StudendaNotification> notifications = <StudendaNotification>[
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00"),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00"),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00"),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00"),
];

class MainScreenWidget extends StatefulWidget {
  const MainScreenWidget({super.key});

  @override
  State<MainScreenWidget> createState() => _MainScreenWidgetState();
}

class _MainScreenWidgetState extends State<MainScreenWidget> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color.fromARGB(255, 240, 241, 245),
      appBar: AppBar(
        leading: IconButton(
          icon: const Icon(Icons.chevron_left_sharp),
          onPressed: () => {},
        ),
        titleSpacing: 0,
        centerTitle: true,
        title: const Text(
          'Главная',
          style: TextStyle(color: Colors.white, fontSize: 25),
        ),
        actions: [
          IconButton(
            onPressed: () => {},
            icon: const Icon(Icons.notifications),
          ),
        ],
      ),
      body: SingleChildScrollView(
        physics: const ClampingScrollPhysics(),
        child: Padding(
          padding: const EdgeInsets.all(14.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                "Расписание",
                style: TextStyle(
                  color: mainForegroundColor,
                  fontSize: 20,
                  fontWeight: FontWeight.w600,
                ),
              ),
              const SizedBox(height: 20),
              DayScheduleWidget(subjects: schedule),
              const SizedBox(height: 25),
              const Text(
                "Уведомления",
                style: TextStyle(
                  color: mainForegroundColor,
                  fontSize: 20,
                  fontWeight: FontWeight.w600,
                ),
              ),
              const SizedBox(height: 20),
              NotificationListWidget(notifications: notifications.take(maxNotificationVisibleOnMainScreen).toList()),
              const SizedBox(height: 25),
              const Text(
                "Экзамены",
                style: TextStyle(
                  color: mainForegroundColor,
                  fontSize: 20,
                  fontWeight: FontWeight.w600,
                ),
              ),
              const SizedBox(height: 20),
              NotificationListWidget(notifications: notifications.take(maxNotificationVisibleOnMainScreen).toList()),
            ],
          ),
        ),
      ),
    );
  }
}


