import 'package:flutter/material.dart';
import 'package:studenda_mobile/model/common/notification.dart';
import 'package:studenda_mobile/model/schedule/subject.dart';
import 'package:studenda_mobile/resourses/colors.dart';
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
      body: Padding(
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
            const SizedBox(height: 43),
            const Text(
              "Уведомления",
              style: TextStyle(
                color: mainForegroundColor,
                fontSize: 20,
                fontWeight: FontWeight.w600,
              ),
            ),
            const SizedBox(height: 20),
            NotificationListWidget(notifications: notifications),
          ],
        ),
      ),
    );
  }
}

class NotificationListWidget extends StatelessWidget {
  final List<StudendaNotification> notifications;

  const NotificationListWidget({super.key, required this.notifications});

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.spaceEvenly,
      children: notifications
          .map((element) => NotificationItemWidget(notification: element))
          .toList(),
    );
  }
}

class NotificationItemWidget extends StatelessWidget {
  final StudendaNotification notification;

  const NotificationItemWidget({super.key, required this.notification});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 5),
      child: IntrinsicHeight(
        child: Container(
          decoration: BoxDecoration(
            border: Border.all(
              color: Colors.white,
            ),
            color: Colors.white,
            borderRadius: const BorderRadius.all(Radius.circular(5)),
          ),
          child: Padding(
            padding: const EdgeInsets.all(10.0),
            child: Row(
              children: [
                Expanded(
                  child: Text(
                    notification.title,
                    style: const TextStyle(
                      color: mainForegroundColor,
                      fontSize: 16,
                    ),
                  ),
                ),
                Text(
                  notification.date,
                  style: const TextStyle(
                    color: mainForegroundColor,
                    fontSize: 16,
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
