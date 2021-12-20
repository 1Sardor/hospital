from rest_framework.decorators import action, api_view
from django.contrib.auth import authenticate
from rest_framework.authentication import TokenAuthentication
from rest_framework.authtoken.models import Token
from rest_framework.response import Response
from rest_framework import viewsets
from .serializer import *
from main.models import *
from rest_framework.permissions import IsAuthenticated
from django.db.models import Q
import requests

@api_view(['POST'])
def Login(request):
    try:
        username = request.data['username']
        password = request.data['password']
        try:
            usrs = Doctor.objects.get(username=username)
            user = authenticate(username=username, password=password)
            if user is not None:
                status = 200
                token, created = Token.objects.get_or_create(user=user)
                data = {
                    'status': status,
                    'username': username,
                    'user_id': usrs.id,
                    'token': token.key,
                    'phone': usrs.phone,
                    'direction': usrs.direction.name,
                    'hospital': usrs.hospital.name,
                    'hospital type': usrs.hospital.type,
                }
            else:
                status = 403
                message = 'Username yoki parol xato!'
                data = {
                    'status': status,
                    'message': message,
                }
        except Doctor.DoesNotExist:
            status = 404
            message = 'Bunday foydalanuvchi mavjud emas!'
            data = {
                'status': status,
                'message': message,
            }
        return Response(data)
    except Exception as er:
        return Response({"error": f'{er}'})


class DoctorView(viewsets.ModelViewSet):
    queryset = Doctor.objects.all()
    serializer_class = DoctorSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)

    def create(self, request):
        try:
            username = request.data['username']
            password = request.data['password']
            direction_id = request.data['direction']
            hospital_id = request.data['hospital']
            phone = request.data['phone']
            user = Doctor.objects.create_user(username=username, password=password, phone=phone, direction_id=direction_id, hospital_id=hospital_id,)
            ser = self.serializer_class(user)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})


    @action(methods=['POST'], detail=False)
    def next_turn(self, request):
        try:
            user = request.user
            last = Queue.objects.filter(direction=user.direction, active=True).first()
            if last is None:
                return Response({"message": "Navbat yo'q"})
            last.active = False
            last.save()
            requests.get(url=f"http://192.168.67.104:8000/api/queue/get_last/?direction_id={user.direction.id}&hospital_id={user.hospital.id}")
            return Response({"message": "done"})
        except Exception as err:
            return Response({'error': f'{err}'})

    @action(methods=['GET'], detail=False)
    def get_hospital_doctors(self, request):
        try:
            hospital_id = request.data['hospital']
            query = self.queryset.filter(hospital_id=hospital_id)
            ser = self.serializer_class(query, many=True)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})


class DirectionView(viewsets.ModelViewSet):
    queryset = Direction.objects.all()
    serializer_class = DirectionSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)


class ProvinceView(viewsets.ModelViewSet):
    queryset = Province.objects.all()
    serializer_class = ProvinceSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)


class RegionView(viewsets.ModelViewSet):
    queryset = Region.objects.all()
    serializer_class = RegionSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)

    @action(methods=['GET'], detail=False)
    def get_province_region(self, request):
        try:
            province_id = request.data['province']
            query = self.queryset.filter(province_id=province_id)
            ser = self.serializer_class(query, many=True)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})


class HospitalView(viewsets.ModelViewSet):
    queryset = Hospital.objects.all()
    serializer_class = HospitalSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)


class PatientView(viewsets.ModelViewSet):
    queryset = Patient.objects.all()
    serializer_class = PatientSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)

    @action(methods=['GET'], detail=False)
    def search(self, request):
        try:
            user = request.user
            search = request.GET['search']
            query = self.queryset.filter(name__icontains=search, hospital=user.hospital)
            ser = self.serializer_class(query, many=True)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})


class RetseptsView(viewsets.ModelViewSet):
    queryset = Retsepts.objects.all()
    serializer_class = RetseptsSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)

    @action(methods=['GET'], detail=False)
    def get_retsepts_of_doctor(self, request):
        try:
            user = request.user
            query = self.queryset.filter(doctor=user)
            ser = self.serializer_class(query, many=True)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})

    @action(methods=['GET'], detail=False)
    def get_retsepts_of_client(self, request):
        try:
            client = request.GET['client']
            query = self.queryset.filter(client=client)
            ser = self.serializer_class(query, many=True)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})


class CommentsView(viewsets.ModelViewSet):
    queryset = Comments.objects.all()
    serializer_class = CommentsSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)

    def create(self, request):
        try:
            patient_id = request.data['patient']
            doctor_id = request.data['doctor']
            ill = request.data['ill']
            com = Comments.objects.create(patient_id=patient_id, doctor_id=doctor_id, ill=ill)
            ser = self.serializer_class(com)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})

    @action(methods=['GET'], detail=False)
    def get_comment_of_client(self, request):
        try:
            client = request.GET['client']
            query = self.queryset.filter(client=client)
            ser = self.serializer_class(query, many=True)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})

    @action(methods=['GET'], detail=False)
    def get_comment_of_doctor(self, request):
        try:
            user = request.user
            query = self.queryset.filter(doctor=user)
            ser = self.serializer_class(query, many=True)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})


class DrugsView(viewsets.ModelViewSet):
    queryset = Drugs.objects.all()
    serializer_class = DrugsSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)

    def create(self, request):
        try:
            retsept_id = request.data['retsept']
            name = request.data['name']
            duration = request.data['duration']
            drug = Drugs.objects.create(retsept_id=retsept_id, name=name, duration=duration)
            ser = self.serializer_class(drug)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})

    @action(methods=['GET'], detail=False)
    def get_by_retsep(self, request):
        try:
            retsept_id = request.GET['retsept']
            query = self.queryset.filter(retsept_id=retsept_id)
            ser = self.serializer_class(query, many=True)
            return Response(ser.data)
        except Exception as err:
            return Response({"error": f'{err}'})


class PaymentView(viewsets.ModelViewSet):
    queryset = Payment.objects.all()
    serializer_class = PaymentSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)

    def create(self, request):
        try:
            patient_id = request.data['patient']
            summa = request.data['summa']
            payment = Payment.objects.create(patient_id=patient_id, summa=summa)
            query = self.serializer_class(payment)
            return Response(query.data)
        except Exception as err:
            return Response({'error': f'{err}'})


class PaymentTypesView(viewsets.ModelViewSet):
    queryset = PaymentTypes.objects.all()
    serializer_class = PaymentTypesSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)

    def create(self, request):
        try:
            payment_id = request.data['payment']
            name = request.data['name']
            summa = request.data['summa']
            payment = Payment.objects.get(id=payment_id)
            payment.summa += summa
            payment.save()
            types = PaymentTypes.objects.create(payment_id=payment_id, name=name, summa=summa)
            query = self.serializer_class(types)
            return Response(query.data)
        except Exception as err:
            return Response({'error': f'{err}'})

    @action(methods=['GET'], detail=False)
    def get_types(self, request):
        try:
            payment_id = request.GET['payment']
            query = self.queryset.filter(payment_id=payment_id)
            ser = self.serializer_class(query, many=True)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})


class QueueView(viewsets.ModelViewSet):
    queryset = Queue.objects.all()
    serializer_class = QueueSerializer
    # authentication_classes = (TokenAuthentication,)
    # permission_classes = (IsAuthenticated,)

    @action(methods=['GET'], detail=False)
    def get_last(self, request):
        try:
            hospital_id = request.GET['hospital_id']
            direction_id = request.GET['direction_id']
            last = self.queryset.filter(direction_id=direction_id, active=True, hospital_id=hospital_id).first()
            ser = self.serializer_class(last)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})
