import 'package:flutter/material.dart';
import 'package:studenda_mobile/feature/notification/presentation/widgets/notification_consts.dart';
import 'package:studenda_mobile/feature/notification/presentation/widgets/notification_list_widget.dart';
import 'package:studenda_mobile/feature/schedule/domain/entities/subject_entity.dart';
import 'package:studenda_mobile/feature/schedule/presentation/widgets/day_schedule_widget.dart';
import 'package:studenda_mobile/model/common/notification.dart';
import 'package:studenda_mobile/resources/colors.dart';

final List<SubjectEntity> schedule = <SubjectEntity>[
  SubjectEntity(0, "Математика", "ВЦ-315", "QWERTY"),
  SubjectEntity(1, "Физкультура", "ВЦ-315", "QWERTY"),
  SubjectEntity(2, "Базы данных", "ВЦ-315", "QWERTY"),
  SubjectEntity(3, "Экономика", "ВЦ-315", "QWERTY"),
];

final List<StudendaNotification> notifications = <StudendaNotification>[
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
];

class HomeScreenWidget extends StatefulWidget {
  const HomeScreenWidget({super.key});

  @override
  State<HomeScreenWidget> createState() => _HomeScreenWidgetState();
}

class _HomeScreenWidgetState extends State<HomeScreenWidget> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color.fromARGB(255, 240, 241, 245),
      appBar: AppBar(
        titleSpacing: 0,
        centerTitle: true,
        automaticallyImplyLeading: false,
        title: const Text(
          'Главная',
          style: TextStyle(color: Colors.white, fontSize: 25),
        ),
        actions: [
          IconButton(
            onPressed: () => {Navigator.of(context).pushNamed('/notification')},
            icon: const Icon(Icons.notifications, color: Colors.white,),
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
              DayScheduleWidget(dayTitle: "Сегодня",subjects: schedule, isTitleRequired: false),
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
