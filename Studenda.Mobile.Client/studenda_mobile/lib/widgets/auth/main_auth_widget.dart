import 'package:flutter/material.dart';
import 'package:studenda_mobile/resourses/colors.dart';
import 'package:studenda_mobile/widgets/UI/button_widget.dart';

class MainAuthWidget extends StatefulWidget {
  const MainAuthWidget({super.key});

  @override
  State<MainAuthWidget> createState() => _MainAuthWidgetState();
}

class _MainAuthWidgetState extends State<MainAuthWidget> {

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: mainAuthBackgroundColor,
      body: Container(
        alignment: AlignmentDirectional.center,
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            const Text(
              "СТУДЕНДА",
              style: TextStyle(color: mainForegroundColor, fontSize: 40),
            ),
            const SizedBox(
              height: 40,
            ),
            StudendaButton(title: "Войти", event: (){}),
            const SizedBox(
              height: 34,
            ),
            StudendaButton(title: "Войти как гость", event: (){}),
          ],
        ),
      ),
    );
  }
}
