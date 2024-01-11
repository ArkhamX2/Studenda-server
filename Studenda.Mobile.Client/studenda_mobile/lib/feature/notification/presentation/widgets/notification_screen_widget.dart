import 'package:flutter/material.dart';
import 'package:studenda_mobile/feature/notification/presentation/widgets/notification_list_widget.dart';
import 'package:studenda_mobile/model/common/notification.dart';


final List<StudendaNotification> notifications = <StudendaNotification>[
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
      StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
      StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
      StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
      StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
      StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),
  StudendaNotification(
      title: "Текст уведомления", description: "", date: "23.04 17:00",),

];


class NotificationScreenWidget extends StatefulWidget {
  const NotificationScreenWidget({super.key});

  @override
  State<NotificationScreenWidget> createState() => _NotificationScreenWidgetState();
}

class _NotificationScreenWidgetState extends State<NotificationScreenWidget> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color.fromARGB(255, 240, 241, 245),
      appBar: AppBar(
        leading: IconButton(
          icon: const Icon(Icons.chevron_left_sharp),
          color: Colors.white,
          onPressed: () => {Navigator.of(context).pop()},
        ),
        titleSpacing: 0,
        centerTitle: true,
        title: const Text(
          'Уведомления',
          style: TextStyle(color: Colors.white, fontSize: 25),
        ),
      ),
      body: Padding(
        padding: const EdgeInsets.all(14.0),
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const SizedBox(height: 17),
              NotificationListWidget(notifications: notifications),
            ],
          ),
        ),
      ),
    );
  }
}
